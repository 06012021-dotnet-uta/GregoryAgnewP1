using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseModels;
using ViewModels;

namespace P1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Context _context;

        public CategoryController(Context context)
        {
            _context = context;
        }

        

        // GET: Cat1
        public IActionResult Index()
        {
            return View(_context);
        }

        // GET: Cat1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat1 = await _context.Cat1s
                .FirstOrDefaultAsync(m => m.Cat1id == id);
            if (cat1 == null)
            {
                return NotFound();
            }

            return View(cat1);
        }

        // GET: Cat1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cat1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cat1id,Category1")] Cat1 cat1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cat1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cat1);
        }

        // GET: Cat1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat1 = await _context.Cat1s.FindAsync(id);
            if (cat1 == null)
            {
                return NotFound();
            }
            return View(cat1);
        }

        // POST: Cat1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cat1id,Category1")] Cat1 cat1)
        {
            if (id != cat1.Cat1id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cat1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Cat1Exists(cat1.Cat1id))
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
            return View(cat1);
        }

        // GET: Cat1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat1 = await _context.Cat1s
                .FirstOrDefaultAsync(m => m.Cat1id == id);
            if (cat1 == null)
            {
                return NotFound();
            }

            return View(cat1);
        }

        // POST: Cat1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cat1 = await _context.Cat1s.FindAsync(id);
            _context.Cat1s.Remove(cat1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Cat1Exists(int id)
        {
            return _context.Cat1s.Any(e => e.Cat1id == id);
        }
    }
}
