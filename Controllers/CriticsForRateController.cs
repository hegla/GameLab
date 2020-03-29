using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sem2Lab1SQLServer;

namespace Sem2Lab1SQLServer.Controllers
{
    public class CriticsForRateController : Controller
    {
        private readonly gameindustryContext _context;

        public CriticsForRateController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: CriticsForRate
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Ratings");
            ViewBag.Gameid = id;
            var criticForRate = _context.Critics.Where(x => x.CriticId == id);
            return View(await criticForRate.ToListAsync());
        }

        // GET: CriticsForRate/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var critics = await _context.Critics
                .FirstOrDefaultAsync(m => m.CriticId == id);
            if (critics == null)
            {
                return NotFound();
            }

            return View(critics);
        }

        // GET: CriticsForRate/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CriticsForRate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CriticId,Username")] Critics critics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(critics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(critics);
        }

        // GET: CriticsForRate/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var critics = await _context.Critics.FindAsync(id);
            if (critics == null)
            {
                return NotFound();
            }
            return View(critics);
        }

        // POST: CriticsForRate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CriticId,Username")] Critics critics)
        {
            if (id != critics.CriticId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(critics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriticsExists(critics.CriticId))
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
            return View(critics);
        }

        // GET: CriticsForRate/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var critics = await _context.Critics
                .FirstOrDefaultAsync(m => m.CriticId == id);
            if (critics == null)
            {
                return NotFound();
            }

            return View(critics);
        }

        // POST: CriticsForRate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var critics = await _context.Critics.FindAsync(id);
            _context.Critics.Remove(critics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriticsExists(int id)
        {
            return _context.Critics.Any(e => e.CriticId == id);
        }
    }
}
