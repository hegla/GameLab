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
    public class RatingsForCriticsController : Controller
    {
        private readonly gameindustryContext _context;

        public RatingsForCriticsController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: RatingsForCritics
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Critics");
            ViewBag.CriticName = name;
            var rateForCritic = _context.Ratings.Where(x => x.CriticId == id).Include(r => r.Critic).Include(r => r.Game);
            return View(await rateForCritic.ToListAsync());
        }

        // GET: RatingsForCritics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratings = await _context.Ratings
                .Include(r => r.Critic)
                .Include(r => r.Game)
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (ratings == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "GamesForRate", new { id = ratings.GameId });
        }

        // GET: RatingsForCritics/Create
        public IActionResult Create()
        {
            ViewData["CriticId"] = new SelectList(_context.Critics, "CriticId", "Username");
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Name");
            return View();
        }

        // POST: RatingsForCritics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RatingId,CriticId,GameId,Mark")] Ratings ratings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ratings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CriticId"] = new SelectList(_context.Critics, "CriticId", "Username", ratings.CriticId);
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Name", ratings.GameId);
            return View(ratings);
        }

        // GET: RatingsForCritics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratings = await _context.Ratings.FindAsync(id);
            if (ratings == null)
            {
                return NotFound();
            }
            ViewData["CriticId"] = new SelectList(_context.Critics, "CriticId", "Username", ratings.CriticId);
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Name", ratings.GameId);
            return View(ratings);
        }

        // POST: RatingsForCritics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RatingId,CriticId,GameId,Mark")] Ratings ratings)
        {
            if (id != ratings.RatingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingsExists(ratings.RatingId))
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
            ViewData["CriticId"] = new SelectList(_context.Critics, "CriticId", "Username", ratings.CriticId);
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Name", ratings.GameId);
            return View(ratings);
        }

        // GET: RatingsForCritics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratings = await _context.Ratings
                .Include(r => r.Critic)
                .Include(r => r.Game)
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (ratings == null)
            {
                return NotFound();
            }

            return View(ratings);
        }

        // POST: RatingsForCritics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratings = await _context.Ratings.FindAsync(id);
            _context.Ratings.Remove(ratings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingsExists(int id)
        {
            return _context.Ratings.Any(e => e.RatingId == id);
        }
    }
}
