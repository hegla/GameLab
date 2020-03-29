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
    public class ContForCountryController : Controller
    {
        private readonly gameindustryContext _context;

        public ContForCountryController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: ContForCountry
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Countries");
            var gameindustryContext = _context.Continents.Where(x => x.ContinentId == id);
            return View(await gameindustryContext.ToListAsync());
        }

        // GET: ContForCountry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continents = await _context.Continents
                .FirstOrDefaultAsync(m => m.ContinentId == id);
            if (continents == null)
            {
                return NotFound();
            }

            return View(continents);
        }

        // GET: ContForCountry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContForCountry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContinentId,Name,Area")] Continents continents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(continents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(continents);
        }

        // GET: ContForCountry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continents = await _context.Continents.FindAsync(id);
            if (continents == null)
            {
                return NotFound();
            }
            return View(continents);
        }

        // POST: ContForCountry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContinentId,Name,Area")] Continents continents)
        {
            if (id != continents.ContinentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(continents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContinentsExists(continents.ContinentId))
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
            return View(continents);
        }

        // GET: ContForCountry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continents = await _context.Continents
                .FirstOrDefaultAsync(m => m.ContinentId == id);
            if (continents == null)
            {
                return NotFound();
            }

            return View(continents);
        }

        // POST: ContForCountry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var continents = await _context.Continents.FindAsync(id);
            _context.Continents.Remove(continents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContinentsExists(int id)
        {
            return _context.Continents.Any(e => e.ContinentId == id);
        }
    }
}
