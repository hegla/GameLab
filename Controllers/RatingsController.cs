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
    public class RatingsController : Controller
    {
        private readonly gameindustryContext _context;

        public RatingsController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var gameindustryContext = _context.Ratings.Include(r => r.Critic).Include(r => r.Game);
            return View(await gameindustryContext.ToListAsync());
        }

        // GET: Ratings/Details/5
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

            return RedirectToAction("Index", "CriticsForRate", new { id = ratings.CriticId });
        }
        public async Task<IActionResult> GameDetails(int? id)
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

            return RedirectToAction("Index", "GamesForRate", new { id = ratings.GameId});
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["CriticId"] = new SelectList(_context.Critics, "CriticId", "Username");
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Name");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RatingId,CriticId,GameId,Mark")] Ratings ratings)
        {
            bool duplicate = await _context.Ratings.AnyAsync(l => l.CriticId.Equals(ratings.CriticId) && l.GameId.Equals(ratings.GameId));
            if (duplicate)
            {
                ModelState.AddModelError("", "Цей критик вже оцінив цю гру");
            }
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

        // GET: Ratings/Edit/5
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

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RatingId,CriticId,GameId,Mark")] Ratings ratings)
        {
            bool duplicate = await _context.Ratings.AnyAsync(l => l.CriticId.Equals(ratings.CriticId) && l.GameId.Equals(ratings.GameId) && !l.RatingId.Equals(ratings.RatingId));
            if(duplicate)   
            {
                ModelState.AddModelError("", "Така оцінка вже існує");
            }
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

        // GET: Ratings/Delete/5
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

        // POST: Ratings/Delete/5
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
