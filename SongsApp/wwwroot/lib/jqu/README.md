jQueue
=================================================================
jQueue is a very tiny piece of code that provides a call interface with period limit.

In original Javascript, it has a built-in queue management to manage client calls. But it's less control.

For example, you want to check user input with Ajax for give user hints in real time, you need to bind 'Keyup' or 'Keydown' event listener to Input element.

Which is fine, but real problem is, when user start typing, the Javascript will constantly send Ajax query to your interface, and it's not a good news for your relatively slow Web App.

Now you have jQueue, you can simply define the Ajax function with period limit, and now your function only will be call in each limited period, perfectly solved above problem! Amazing Isn't?

Amazingly simple, You can use it like this
-----------------------------------------------------------------

Let's see how to handle with that Ajax problem.

	var q = new jQueue(function(type, name, callback) {
		$.post("{% $RootURL %}/api/account/user_existed/", {Method: type, Value: name}).done(callback);
	}, 'Alter', 1000);

As you can see, we using jQuery for our Ajax query because it's save our lot's of lines, But jQueue can work with any Javascript framework or pure Javascript.

Now, you can just call the function like this:

	q.run('Username', object.val(), function(data){ alert(data); });

If you want to get result of registered function, you can continue chain the call like this:

	var q = new jQueue(function(type, name, callback) {
		return '<Something rightway>';
	}, 'Alter', 1000);

	q.run('Username', object.val(), q.callback(function(data){
		alert(data);
	})).done(function(data) {
	 	/* Only be call when it's done */
	}).fail(function(data) {
		/* Only be call when it's failed */
	}).result(function(data) {
		/* Return whatever the result is */
	}).callback(function(callbackID, callbackResult) {
		/* Run when everytime each on-time callbacks returns a result */
		/* Expired callback will not be called */
	});

Notice we marked our callback function with method `q.callback` (or `q.cb` for short), so jQueue will keep track of that callback to see if it is the lastest one we actually needed.

Install
-----------------------------------------------------------------

### Bower

Add `"jQueue": "~0.3"` to your `dependencies` section in `bower.json`, and run `bower install`.

### npm

Add `"jqu": "~0.3"` to your `dependencies` section in `package.json`, and run `npm install`.

or just `npm install jqu`.


Parameters
-----------------------------------------------------------------

There are 3 parameters for jQueue.

	var q = new jQueue(<A Function>, <Mode>, <Delay>);

The first one is the function callback for your task.

The second one is working mode.

	There are six mode available:

	- Append (Default):
		The queue will be running one by one and stop the queue when all queue rans.

	- Flush:
		If a new request added during queue running, stop current query and discard remaining queue, forcely use the newset one.

	- Last:
		Just like the Flush option, but it will also discard expired callbacks

	- Block:
		Block other request when there is request still waiting for callback

	- Loop:
		Run queue will running forever in a loop once added.

	- <A Number>:
		Run queue will running N times in a loop before it been clear.

The last is the period in ms, set 1000 for 1 second, 2000 for 2 second etc.
