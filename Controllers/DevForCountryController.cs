﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sem2Lab1SQLServer;

namespace Sem2Lab1SQLServer.Controllers
{
    public class DevForCountryController : Controller
    {
        private readonly gameindustryContext _context;

        public DevForCountryController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: DevForCountry
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Countries");
            ViewBag.CountryId = id;
            ViewBag.Name = name;
            var devForCountry = _context.Developers.Where(x => x.CountryId == id).Include(d => d.Country);
            return View(await devForCountry.ToListAsync());
        }

        // GET: DevForCountry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developers = await _context.Developers
                .Include(d => d.Country)
                .FirstOrDefaultAsync(m => m.DeveloperId == id);
            if (developers == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "GamesForDeveloper", new { id = developers.DeveloperId, name = developers.Name });
        }

        // GET: DevForCountry/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Continent");
            return View();
        }

        // POST: DevForCountry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeveloperId,Name,FoundationDate,WorkersNumber,CountryId")] Developers developers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(developers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Continent", developers.CountryId);
            return View(developers);
        }

        // GET: DevForCountry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developers = await _context.Developers.FindAsync(id);
            if (developers == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Continent", developers.CountryId);
            return View(developers);
        }

        // POST: DevForCountry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeveloperId,Name,FoundationDate,WorkersNumber,CountryId")] Developers developers)
        {
            if (id != developers.DeveloperId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(developers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevelopersExists(developers.DeveloperId))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Continent", developers.CountryId);
            return View(developers);
        }

        // GET: DevForCountry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developers = await _context.Developers
                .Include(d => d.Country)
                .FirstOrDefaultAsync(m => m.DeveloperId == id);
            if (developers == null)
            {
                return NotFound();
            }

            return View(developers);
        }

        // POST: DevForCountry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var developers = await _context.Developers.FindAsync(id);
            _context.Developers.Remove(developers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DevelopersExists(int id)
        {
            return _context.Developers.Any(e => e.DeveloperId == id);
        }
    }
}
