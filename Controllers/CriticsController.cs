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
    public class CriticsController : Controller
    {
        private readonly gameindustryContext _context;

        public CriticsController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: Critics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Critics.ToListAsync());
        }

        // GET: Critics/Details/5
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

            return RedirectToAction("Index", "RatingsForCritics", new { id = critics.CriticId, name = critics.Username });
        }

        // GET: Critics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Critics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CriticId,Username,Description")] Critics critics)
        {
            bool duplicate = await _context.Critics.AnyAsync(l => l.Username.Equals(critics.Username));

            if (duplicate)
            {
                ModelState.AddModelError("Username", "Критик с таким ім'ям вже доданий");
            }
            if (ModelState.IsValid)
            {
                _context.Add(critics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(critics);
        }

        // GET: Critics/Edit/5
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

        // POST: Critics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CriticId,Username,Description")] Critics critics)
        {
            bool duplicate = await _context.Critics.AnyAsync(l => l.Username.Equals(critics.Username) && !l.CriticId.Equals(critics.CriticId));
            if (duplicate)
            {
                ModelState.AddModelError("", "Такий критик вже доданий");
            }
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

        // GET: Critics/Delete/5
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

        // POST: Critics/Delete/5
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
