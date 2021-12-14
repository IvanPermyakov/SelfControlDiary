using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfControlDiary.Models;

namespace SelfControlDiary.Controllers
{
    public class PhysicalDiariesController : Controller
    {
        private readonly DiaryContext _context;
        private DiaryContext db;
        public PhysicalDiariesController(DiaryContext context)
        {
            _context = context;
            db = context;
    }

        // GET: PhysicalDiaries
        public async Task<IActionResult> Index()
        {
            var diaryContext = db.PhysicalDiaries.Include(p => p.Physicals);
            return View(db.PhysicalDiaries.ToList().Where(c => c.UserId == User.Identity.Name));
        }

        // GET: PhysicalDiaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physicalDiary = await _context.PhysicalDiaries
                .Include(p => p.Physicals)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (physicalDiary == null)
            {
                return NotFound();
            }

            return View(physicalDiary);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SelectList physicals = new SelectList(db.Physicals, "Id", "Exercise");
            ViewBag.Physicals = physicals;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,PhysicalsId,Min,Kall,UserId,Id")] PhysicalDiary physicalDiary)
        {
            if (ModelState.IsValid)
            {
                People people = db.Peoples.ToList().Find(c => c.UserId == User.Identity.Name);
                Physical physical = db.Physicals.ToList().Find(c => c.Id == physicalDiary.PhysicalsId);
                physicalDiary.UserId = User.Identity.Name;
                physicalDiary.weight = people.weight;
                physicalDiary.Kall = (int)(people.weight * physicalDiary.Min * physical.Kall / 60);
                db.PhysicalDiaries.Add(physicalDiary);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(physicalDiary);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PhysicalDiary physicalDiary = db.PhysicalDiaries.Find(id);
            if (physicalDiary != null)
            {
                SelectList physicals = new SelectList(db.Physicals, "Id", "Exercise", physicalDiary.PhysicalsId);
                ViewBag.Physicals = physicals;
                return View(physicalDiary);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Date,PhysicalsId,Min,Kall,UserId,Id")] PhysicalDiary physicalDiary)
        {
            People people = db.Peoples.ToList().Find(c => c.UserId == User.Identity.Name);
            Physical physical = db.Physicals.ToList().Find(c => c.Id == physicalDiary.PhysicalsId);
            physicalDiary.UserId = User.Identity.Name;
            physicalDiary.weight = people.weight;
            physicalDiary.Kall = (int)(people.weight * physicalDiary.Min * physical.Kall / 60);
            db.Entry(physicalDiary).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physicalDiary = await _context.PhysicalDiaries.FirstOrDefaultAsync(m => m.Id == id);
            if (physicalDiary == null)
            {
                return NotFound();
            }

            return View(physicalDiary);
        }

        // POST: PhysicalDiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var physicalDiary = await _context.PhysicalDiaries.FindAsync(id);
            _context.PhysicalDiaries.Remove(physicalDiary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhysicalDiaryExists(int id)
        {
            return _context.PhysicalDiaries.Any(e => e.Id == id);
        }
    }
}
