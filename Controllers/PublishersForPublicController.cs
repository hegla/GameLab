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
    public class PublishersForPublicController : Controller
    {
        private readonly gameindustryContext _context;

        public PublishersForPublicController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: PublishersForPublic
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Publications");
            ViewBag.PublisherId = id;
            var publisherForPublic = _context.Publishers.Where(x => x.PublisherId == id);
            return View(await publisherForPublic.ToListAsync());
        }

        // GET: PublishersForPublic/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishers = await _context.Publishers
                .FirstOrDefaultAsync(m => m.PublisherId == id);
            if (publishers == null)
            {
                return NotFound();
            }

            return View(publishers);
        }

        // GET: PublishersForPublic/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PublishersForPublic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherId,Name,Earnings,Contacts")] Publishers publishers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publishers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publishers);
        }

        // GET: PublishersForPublic/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishers = await _context.Publishers.FindAsync(id);
            if (publishers == null)
            {
                return NotFound();
            }
            return View(publishers);
        }

        // POST: PublishersForPublic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId,Name,Earnings,Contacts")] Publishers publishers)
        {
            if (id != publishers.PublisherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publishers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublishersExists(publishers.PublisherId))
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
            return View(publishers);
        }

        // GET: PublishersForPublic/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishers = await _context.Publishers
                .FirstOrDefaultAsync(m => m.PublisherId == id);
            if (publishers == null)
            {
                return NotFound();
            }

            return View(publishers);
        }

        // POST: PublishersForPublic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publishers = await _context.Publishers.FindAsync(id);
            _context.Publishers.Remove(publishers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublishersExists(int id)
        {
            return _context.Publishers.Any(e => e.PublisherId == id);
        }
    }
}
