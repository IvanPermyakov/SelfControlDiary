using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SelfControlDiary.Models;

namespace SelfControlDiary.Controllers
{
    public class FoodDiariesController : Controller
    {
        private readonly DiaryContext _context;
        private DiaryContext db;

        public FoodDiariesController(DiaryContext context)
        {
            _context = context;
            db = context;
        }
        public IActionResult Index()
        {
            var foods = db.FoodDiaries.Include(p => p.Foods);
            return View(db.FoodDiaries.ToList().Where(c => c.UserId == User.Identity.Name));
        }
        // GET: FoodDiaries

        // GET: FoodDiaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodDiary = await _context.FoodDiaries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodDiary == null)
            {
                return NotFound();
            }

            return View(foodDiary);
        }

        // GET: FoodDiaries/Create
        [HttpGet]
        public IActionResult Create()
        {
            SelectList foods = new SelectList(db.Foods, "Id", "FoodName");
            ViewBag.Foods = foods;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Grams,Id,FoodsId")] FoodDiary foodDiary)
        {
            if (ModelState.IsValid)
            {
                Food food = db.Foods.ToList().Find(c => ( c.UserId == User.Identity.Name || c.UserId == "0" ) && c.Id == foodDiary.FoodsId);
                foodDiary.Kalls = food.Kalls * foodDiary.Grams / 100;
                foodDiary.UserId = User.Identity.Name;
                db.FoodDiaries.Add(foodDiary);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(foodDiary);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FoodDiary foodDiary = db.FoodDiaries.Find(id);
            if (foodDiary != null)
            {
                SelectList foods = new SelectList(db.Foods, "Id", "FoodName", foodDiary.FoodsId);
                ViewBag.Foods = foods;
                return View(foodDiary);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Date,Grams,Id,FoodsId,UserId")] FoodDiary foodDiary)
        {
            Food food = db.Foods.ToList().Find(c => (c.UserId == User.Identity.Name || c.UserId == "0") && c.Id == foodDiary.FoodsId);
            foodDiary.Kalls = food.Kalls * foodDiary.Grams / 100;
            foodDiary.UserId = User.Identity.Name;
            db.Entry(foodDiary).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            /*
            if (id != foodDiary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodDiary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodDiaryExists(foodDiary.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodDiary);*/
        }

        // GET: FoodDiaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodDiary = await _context.FoodDiaries.FirstOrDefaultAsync(m => m.Id == id);
            if (foodDiary == null)
            {
                return NotFound();
            }

            return View(foodDiary);
        }

        // POST: FoodDiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodDiary = await _context.FoodDiaries.FindAsync(id);
            _context.FoodDiaries.Remove(foodDiary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodDiaryExists(int id)
        {
            return _context.FoodDiaries.Any(e => e.Id == id);
        }
    }
}
