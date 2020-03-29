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
    public class PublicationsController : Controller
    {
        private readonly gameindustryContext _context;

        public PublicationsController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: Publications
        public async Task<IActionResult> Index()
        {
            var gameindustryContext = _context.Publications.Include(p => p.Game).Include(p => p.Publisher);
            return View(await gameindustryContext.ToListAsync());
        }

        // GET: Publications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publications = await _context.Publications
                .Include(p => p.Game)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.PublicationId == id);
            if (publications == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "PublishersForPublic", new { id = publications.PublisherId });
        }

        public async Task<IActionResult> GameDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publications = await _context.Publications
                .Include(p => p.Game)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.PublicationId == id);
            if (publications == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "GamesForPublic", new { id = publications.GameId });
        }

        // GET: Publications/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name");
            return View();
        }

        // POST: Publications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublicationId,GameId,PublisherId")] Publications publications)
        {
            bool duplicate = await _context.Publications.AnyAsync(l => l.GameId.Equals(publications.GameId) && l.PublisherId.Equals(publications.PublisherId));

            if (duplicate)
            {
                ModelState.AddModelError ("","Така публікація вже додана");
            }
            if (ModelState.IsValid)
            {
                _context.Add(publications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Name", publications.GameId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name", publications.PublisherId);
            return View(publications);
        }

        // GET: Publications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publications = await _context.Publications.FindAsync(id);
            if (publications == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Name", publications.GameId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name", publications.PublisherId);
            return View(publications);
        }

        // POST: Publications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublicationId,GameId,PublisherId")] Publications publications)
        {
            bool duplicate = await _context.Publications.AnyAsync(l => l.GameId.Equals(publications.GameId) && l.PublisherId.Equals(publications.PublisherId) && !l.PublicationId.Equals(publications.PublicationId));
            if (duplicate)
            {
                ModelState.AddModelError("", "Така публікація вже додана");
            }
            if (id != publications.PublicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicationsExists(publications.PublicationId))
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
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "Name", publications.GameId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "Name", publications.PublisherId);
            return View(publications);
        }

        // GET: Publications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publications = await _context.Publications
                .Include(p => p.Game)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.PublicationId == id);
            if (publications == null)
            {
                return NotFound();
            }

            return View(publications);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publications = await _context.Publications.FindAsync(id);
            _context.Publications.Remove(publications);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicationsExists(int id)
        {
            return _context.Publications.Any(e => e.PublicationId == id);
        }
    }
}
