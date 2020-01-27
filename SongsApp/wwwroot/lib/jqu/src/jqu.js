/*
* jQueue: Javascript Period Queuer
*
* @copyright	2015 Rain Lee <raincious@gmail.com>
* @author		Rain Lee <raincious@gmail.com>
* @package		jQueue
* @version		0.3.1 prototype
*
* Copyright (c) 2015, Rain Lee
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*
* 1. Redistributions of source code must retain the above copyright notice, this
*    list of conditions and the following disclaimer.
* 2. Redistributions in binary form must reproduce the above copyright notice,
*    this list of conditions and the following disclaimer in the documentation
*    and/or other materials provided with the distribution.
*
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
* ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
* ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*
* The views and conclusions contained in the software and documentation are those
* of the authors and should not be interpreted as representing official policies,
* either expressed or implied, of the FreeBSD Project.
*
*/

var jQueue = function(func, type, period) {
    var self = this;
    var inited = false;
    var queues = [];
    var queueIdx = 0;
    var lastParams = null;
    var queueMaxIdx = 9999999999;
    var queuer = null;

    var exec = (typeof func === 'function') ? func : function() {};
    var mode = type == 'Last' || type == 'Block' || type == 'Flush' || type == 'Append' || type == 'Loop' || (typeof type === 'number' && type % 1 == 0) ? type : 'Append';
    var interval = period ? parseInt(period, 10) : 300;

    var start = function() {
        if (queuer !== null) {
            return true;
        }

        queuer = setInterval(function() {
            if (queues.length <= 0) {
                /* Don't clear if last param still waiting */
                if (lastParams !== null && !lastParams.ICC()) {
                    return;
                }

                expireLastParams();

                clearInterval(queuer);
                queuer = null;

                return;
            }

            var params = queues.shift();

            if (params.E) {
                return;
            }

            var result = exec.apply(self, params.P);

            params.Cb.R(result);

            if (result) {
                params.Cb.D(result);
            } else {
                params.Cb.F(result);
            }

            if (params.L != 'Forever' && --params.L <= 0) {
                return;
            }

            queues.push(params);
        }, interval);

        return true;
    };

    var Resultr = function(ID) {
        var self = this;
        var paramID = ID;
        var found = false;

        var getElFromQueue = function() {
            for (var p in queues) {
                if (queues[p].ID != paramID) {
                    continue;
                }

                return p;
            }

            return -1
        };

        var hookCallback = function(type, callback) {
            if (typeof callback !== 'function') {
                return false;
            }

            if ((p = getElFromQueue()) == -1) {
                return false;
            }

            queues[p].Cb[type] = callback;

            return true;
        };

        self.result = function(callback) {
            hookCallback('R', callback);

            return self;
        };

        self.done = function(callback) {
            hookCallback('D', callback);

            return self;
        };

        self.fail = function(callback) {
            hookCallback('F', callback);

            return self;
        };

        self.callback = function(callback) {
            hookCallback('C', callback);

            return self;
        };

        self.expired = function(callback) {
            hookCallback('E', callback);

            return self;
        };

        return true;
    };

    var Callbacker = function(callback) {
        var self = this;
        var jCb = ((typeof callback !== 'function') ? function() {
            return callback;
        } : callback);

        self.export = function() {
            return jCb;
        };
    };

    var alterCallback = function(paramID, params, callback, acbFinished) {
        return function() {
            /* Make this callback as called */
            params.IC[paramID] = true;

            if (params.E) {
                return;
            }

            var cbResult = callback.apply(self, arguments);

            acbFinished(cbResult);
        };
    };

    var expireLastParams = function() {
        if (lastParams === null) {
            return;
        }

        if (!lastParams.E) {
            lastParams.E = true;
            lastParams.Cb.E();
        }

        lastParams = null;
    };

    self.reset = function() {
        expireLastParams();

        if (queuer !== null) {
            clearInterval(queuer);
            queuer = null;
        }

        queues = [];
    };

    self.cb =
    self.callback = function(callback) {
        return new Callbacker(callback);
    };

    self.run = function() {
        var reslt = null;
        var altered = false;
        var flushAddCheck = function() { return true; };
        var params = {
            ID: (queueIdx++) % queueMaxIdx,     /* Parameter ID */
            E: false,                           /* Expired */
            P: arguments,                       /* Arguments */
            IC: {},                             /* Indexes of callbacks in param */
            ICC: function() {                   /* Check if there is any callback in param is not called */
                for (var i in this.IC) {
                    if (!this.IC[i]) {
                        return false;
                    }
                }

                return true;
            },
            L: 1,                               /* Loop Counts */
            Cb: {                               /* Callbacks */
                D: function() {},                   /* Done */
                F: function() {},                   /* Failed */
                R: function() {},                   /* Result */
                C: function() {},                   /* Callback called */
                E: function() {}                    /* Expired */
            }
        };

        switch(mode) {
            case 'Block':
                flushAddCheck = function() {
                    if (!altered) {
                        if (queuer === null) {
                            return true;
                        }
                    } else if (lastParams !== null) {
                        return false;
                    }

                    return true;
                };

            case 'Last':
                for (var arg in params.P) {
                    if (typeof params.P[arg] !== 'object') {
                        continue;
                    }

                    if (!(params.P[arg] instanceof Callbacker)) {
                        continue;
                    }

                    params.P[arg] = alterCallback(
                        arg,
                        params,
                        params.P[arg].export(),
                        function(callbackResult) {
                            params.Cb.C(arg, callbackResult);
                        }
                    );

                    /* Make current callback as not called */
                    params.IC[arg] = false;

                    altered = true;
                }

            case 'Flush':
                if (!flushAddCheck()) {
                    return new Resultr(-1);
                }

                if (queuer !== null) {
                    clearInterval(queuer);
                    queuer = null;
                }

                queues = [];
                break;

            case 'Append':
                break;

            case 'Loop':
                params.L = 'Forever';
                break;

            default:
                if (typeof mode === 'number' && mode % 1 == 0) {
                    params.L = parseInt(mode, 10);
                }
                break;
        }

        if (altered) {
            expireLastParams();
        }

        lastParams = params;

        queues.push(params);

        start();

        return new Resultr(params.ID);
    };

    return true;
};