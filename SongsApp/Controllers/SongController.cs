using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SongsApp.Models;

namespace SongsApp.Controllers
{
    public class SongController : Controller
    {

        SongDb Db;
        public SongController(SongDb _Db)
        {
            Db = _Db;
        }

        public IActionResult Index()
        {
            var songs = Db.Songs.Include("Singers").ToList();


            return View(songs);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var singers = Db.Singers.ToList();

            ViewBag.singers = new SelectList(singers,"SingerId", "SingerName", 1);


            return View();
        }

        [HttpPost]
        public IActionResult Create(Song song)
        {
            if (ModelState.IsValid)
            {
                Db.Add(song);
                Db.SaveChanges();
            }
            
            return View("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var s = Db.Songs.Find(id);

            return View(s);
        }

        [HttpPost]
        public IActionResult Edit(Song song)
        {
            Db.Entry(song).State = EntityState.Modified;
            Db.SaveChanges();

            return View("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var song = Db.Songs.Find(id);

            Db.Entry(song).State = EntityState.Deleted;
            Db.SaveChanges();

            return View("Index");
        }
    }

    


}