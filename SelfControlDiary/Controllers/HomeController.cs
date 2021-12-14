using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SelfControlDiary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DiaryContext _context;
        private DiaryContext db;
        public HomeController(DiaryContext context)
        {
            db = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            People people = db.Peoples.ToList().Find(c => c.UserId == User.Identity.Name);
            var kallsF = 0;
            var kallsA = 0;
            var activs = db.Peoples.Include(p => p.Activs);
            var sexs = db.Peoples.Include(p => p.Sexs);
            foreach(var a in db.FoodDiaries)
            {
                if(a.Date.ToShortDateString() == DateTime.Now.ToShortDateString() && a.UserId == User.Identity.Name)
                    kallsF += a.Kalls;
            }
            ViewBag.kallsF = kallsF;
            foreach (var a in db.PhysicalDiaries)
            {
                if (a.Date.ToShortDateString() == DateTime.Now.ToShortDateString() && a.UserId == User.Identity.Name)
                    kallsA += a.Kall;
            }
            ViewBag.kallsA = kallsA;
            if (people != null)
                ViewBag.kallsR = people.Norma - kallsF;
            return View(db.Peoples.ToList().Where(c => c.UserId == User.Identity.Name));
        }
        [HttpGet]
        public IActionResult Create()
        {
            SelectList peoples = new SelectList(db.Sexs, "Id", "Value");
            ViewBag.Peoples = peoples;
            SelectList peoples2 = new SelectList(db.Activs, "Id", "Value");
            ViewBag.Peoples2 = peoples2;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SexId,Age,weight,height,ActivId,UserId")] People people)
        {
            if (ModelState.IsValid)
            {
                people.UserId = User.Identity.Name;
                db.Peoples.Add(people);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(people);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            People people = db.Peoples.Find(id);
            if (people != null)
            {
                SelectList peoples = new SelectList(db.Sexs, "Id", "Value");
                ViewBag.Peoples = peoples;
                SelectList peoples2 = new SelectList(db.Activs, "Id", "Value");
                ViewBag.Peoples2 = peoples2;
                return View(people);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SexId,Age,weight,height,ActivId,Id,UserId")] People people)
        {
            people.UserId = User.Identity.Name;
            db.Entry(people).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
