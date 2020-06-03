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
    public class GamesForGenreController : Controller
    {
        private readonly gameindustryContext _context;

        public GamesForGenreController(gameindustryContext context)
        {
            _context = context;
        }

        public ActionResult Export(int? id, string? name)
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add(name);

                worksheet.Cell("A1").Value = "Жанр";
                worksheet.Cell("A2").Value = name;
                worksheet.Cell("B1").Value = "Назва гри";
                worksheet.Cell("C1").Value = "Бюджет, $";
                worksheet.Cell("D1").Value = "Розробник";
                worksheet.Row(1).Style.Font.Bold = true;
                var games = _context.Games.Where(x => x.GenreId == id).Include(x => x.Developer).ToList();

                for (int i = 0; i < games.Count; i++)
                {
                    worksheet.Cell(i + 2, 2).Value = games[i].Name;
                    worksheet.Cell(i + 2, 3).Value = games[i].Budget;
                    worksheet.Cell(i + 2, 4).Value = games[i].Developer.Name;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"gamesForGenre_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }

            }
        }

        // GET: GamesForGenre
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) RedirectToAction("Index", "Genres");
            ViewBag.GenreId = id;
            ViewBag.Name = name;
            var gameForGenre = _context.Games.Where(x => x.GenreId == id).Include(g => g.Developer).Include(g => g.Genre);
            return View(await gameForGenre.ToListAsync());
        }

        // GET: GamesForGenre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .Include(g => g.Developer)
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // GET: GamesForGenre/Create
        public IActionResult Create()
        {
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Description");
            return View();
        }

        // POST: GamesForGenre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,Name,DeveloperId,GenreId,Budget")] Games games)
        {
            if (ModelState.IsValid)
            {
                _context.Add(games);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "Name", games.DeveloperId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Description", games.GenreId);
            return View(games);
        }

        // GET: GamesForGenre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games.FindAsync(id);
            if (games == null)
            {
                return NotFound();
            }
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "Name", games.DeveloperId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Description", games.GenreId);
            return View(games);
        }

        // POST: GamesForGenre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,Name,DeveloperId,GenreId,Budget")] Games games)
        {
            if (id != games.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(games);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamesExists(games.GameId))
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
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "Name", games.DeveloperId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Description", games.GenreId);
            return View(games);
        }

        // GET: GamesForGenre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .Include(g => g.Developer)
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // POST: GamesForGenre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var games = await _context.Games.FindAsync(id);
            _context.Games.Remove(games);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamesExists(int id)
        {
            return _context.Games.Any(e => e.GameId == id);
        }
    }
}
