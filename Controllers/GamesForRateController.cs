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
    public class GamesForRateController : Controller
    {
        private readonly gameindustryContext _context;

        public GamesForRateController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: GamesForRate
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Ratings");
            ViewBag.Gameid = id;
            var gameForRate = _context.Games.Where(x => x.GameId == id).Include(g => g.Developer).Include(g => g.Genre);
            return View(await gameForRate.ToListAsync());
        }

        // GET: GamesForRate/Details/5
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

        // GET: GamesForRate/Create
        public IActionResult Create()
        {
            ViewData["DeveloperId"] = new SelectList(_context.Developers, "DeveloperId", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Description");
            return View();
        }

        // POST: GamesForRate/Create
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

        // GET: GamesForRate/Edit/5
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

        // POST: GamesForRate/Edit/5
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

        // GET: GamesForRate/Delete/5
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

        // POST: GamesForRate/Delete/5
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
