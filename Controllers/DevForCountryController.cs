using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
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

        public ActionResult Export(int? id, string? name)
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add(name);

                worksheet.Cell("A1").Value = "Країна";
                worksheet.Cell("A2").Value = name;
                worksheet.Cell("B1").Value = "Розробник";
                worksheet.Cell("C1").Value = "Дата заснування";
                worksheet.Cell("D1").Value = "Кількість працівників";
                worksheet.Row(1).Style.Font.Bold = true;
                var devs = _context.Developers.Where(x => x.CountryId == id).ToList();

                for (int i = 0; i < devs.Count; i++)
                {
                    worksheet.Cell(i + 2, 2).Value = devs[i].Name;
                    worksheet.Cell(i + 2, 3).Value = devs[i].FoundationDate;
                    worksheet.Cell(i + 2, 4).Value = devs[i].WorkersNumber;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"devsForCountry_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }

            }
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
