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
    public class CountryForDeveloperController : Controller
    {
        private readonly gameindustryContext _context;

        public CountryForDeveloperController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: CountryForDeveloper
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "DevForGame");
            ViewBag.CountryId = id;
            var countryByDevelper = _context.Countries.Where(x => x.CountryId == id).Include(x => x.Continent);
            return View(await countryByDevelper.ToListAsync());
        }

        // GET: CountryForDeveloper/Details/5
        public async Task<IActionResult> ContDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.Countries
                .Include(c => c.Continent)
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (countries == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "ContForCountry", new { id = countries.ContinentId });
        }

        // GET: CountryForDeveloper/Create
        public IActionResult Create()
        {
            ViewData["ContinentId"] = new SelectList(_context.Continents, "ContinentId", "Name");
            return View();
        }

        // POST: CountryForDeveloper/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,Name,ContinentId")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContinentId"] = new SelectList(_context.Continents, "ContinentId", "Name", countries.ContinentId);
            return View(countries);
        }

        // GET: CountryForDeveloper/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.Countries.FindAsync(id);
            if (countries == null)
            {
                return NotFound();
            }
            ViewData["ContinentId"] = new SelectList(_context.Continents, "ContinentId", "Name", countries.ContinentId);
            return View(countries);
        }

        // POST: CountryForDeveloper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryId,Name,ContinentId")] Countries countries)
        {
            if (id != countries.CountryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountriesExists(countries.CountryId))
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
            ViewData["ContinentId"] = new SelectList(_context.Continents, "ContinentId", "Name", countries.ContinentId);
            return View(countries);
        }

        // GET: CountryForDeveloper/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.Countries
                .Include(c => c.Continent)
                .FirstOrDefaultAsync(m => m.CountryId == id);
            if (countries == null)
            {
                return NotFound();
            }

            return View(countries);
        }

        // POST: CountryForDeveloper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var countries = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(countries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountriesExists(int id)
        {
            return _context.Countries.Any(e => e.CountryId == id);
        }
    }
}
