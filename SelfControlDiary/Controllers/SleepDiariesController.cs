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
    public class SleepDiariesController : Controller
    {
        private readonly DiaryContext _context;

        public SleepDiariesController(DiaryContext context)
        {
            _context = context;
        }

        // GET: SleepDiaries
        public IActionResult Index()
        {
            return View(_context.SleepDiaries.ToList().Where(c => c.UserId == User.Identity.Name));
        }

        // GET: SleepDiaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sleepDiary = await _context.SleepDiaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sleepDiary == null)
            {
                return NotFound();
            }

            return View(sleepDiary);
        }

        // GET: SleepDiaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SleepDiaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Lay,Up,HourSleep,Grade,UserId,Id")] SleepDiary sleepDiary)
        {
            if (ModelState.IsValid)
            {
                sleepDiary.UserId = User.Identity.Name;
                sleepDiary.HourSleep = sleepDiary.Up.Subtract(sleepDiary.Lay);
                _context.Add(sleepDiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sleepDiary);
        }

        // GET: SleepDiaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sleepDiary = await _context.SleepDiaries.FindAsync(id);
            if (sleepDiary == null)
            {
                return NotFound();
            }
            return View(sleepDiary);
        }

        // POST: SleepDiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Lay,Up,HourSleep,Grade,UserId,Id")] SleepDiary sleepDiary)
        {
            sleepDiary.HourSleep = sleepDiary.Up.Subtract(sleepDiary.Lay);
            sleepDiary.UserId = User.Identity.Name;
            _context.Entry(sleepDiary).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SleepDiaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sleepDiary = await _context.SleepDiaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sleepDiary == null)
            {
                return NotFound();
            }

            return View(sleepDiary);
        }

        // POST: SleepDiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sleepDiary = await _context.SleepDiaries.FindAsync(id);
            _context.SleepDiaries.Remove(sleepDiary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SleepDiaryExists(int id)
        {
            return _context.SleepDiaries.Any(e => e.Id == id);
        }
    }
}
