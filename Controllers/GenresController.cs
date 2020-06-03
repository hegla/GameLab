using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sem2Lab1SQLServer;

namespace Sem2Lab1SQLServer.Controllers
{
    public class GenresController : Controller
    {
        private readonly gameindustryContext _context;

        public GenresController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genres.ToListAsync());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genres = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genres == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "GamesForGenre", new { id = genres.GenreId, name = genres.Name }); ;
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreId,Name,Description")] Genres genres)
        {
            bool duplicate = await _context.Genres.AnyAsync(l => l.Name.Equals(genres.Name));

            if (duplicate)
            {
                ModelState.AddModelError("Name", "Такий жанр вже доданий");
            }
            if (ModelState.IsValid)
            {
                _context.Add(genres);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genres);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genres = await _context.Genres.FindAsync(id);
            if (genres == null)
            {
                return NotFound();
            }
            return View(genres);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GenreId,Name,Description")] Genres genres)
        {
            bool duplicate = await _context.Genres.AnyAsync(l => l.Name.Equals(genres.Name) && !l.GenreId.Equals(genres.GenreId));

            if (duplicate)
            {
                ModelState.AddModelError("", "Такий жанр вже існує");
            }
            if (id != genres.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genres);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenresExists(genres.GenreId))
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
            return View(genres);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genres = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genres == null)
            {
                return NotFound();
            }

            return View(genres);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genres = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(genres);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenresExists(int id)
        {
            return _context.Genres.Any(e => e.GenreId == id);
        }

        //Import 
        public IActionResult Import(bool errorFlag, string error)
        {
            if (!errorFlag)
            {
                ViewBag.Error = "Оберіть Excel-файл";
            }
            else
            {
                ViewBag.Error = error;
                ViewBag.ErrorPopupFlag = 1;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (fileExcel != null)
            {
                using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                {
                    await fileExcel.CopyToAsync(stream);
                    using (XLWorkbook workbook = new XLWorkbook(stream, XLEventTracking.Disabled))
                    {
                        if (!ParseDocument(workbook, out string error))
                        {
                            return RedirectToAction("Import", new { errorFlag = true, error = error });
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("Import", new { errorFlag = true, error = "Файл відсутній, спробуйте ще раз" });
            }

            return RedirectToAction("Index", "Genres");
        }

        private bool ParseDocument(XLWorkbook workbook, out string error)
        {
            error = "";
            foreach (IXLWorksheet worksheet in workbook.Worksheets)
            {
                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                {
                    if (!ParseRow(row, out error))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool ParseRow(IXLRow row, out string error)
        {
            Genres genre = new Genres();
            string name = row.Cell(1).Value.ToString();
            string descr = row.Cell(2).Value.ToString();

            string regex = @"^[А-ЯІЇЄа-яіїєA-Za-z'-'' ']*$";

            if (Regex.IsMatch(name, regex, RegexOptions.IgnoreCase))
            {
                bool dublicate = _context.Genres.Any(l => l.Name.Equals(name));
                if (dublicate)
                {
                    error = "Наявна існуюча в базі назва";
                    return false;
                }
                else
                {
                    genre.Name = name;
                }
            }
            else
            {
                error = "У файлі наявне некоректна назва";
                return false;
            }


            if (Regex.IsMatch(descr, regex, RegexOptions.IgnoreCase))
            {
                genre.Description = descr;
            }
            else
            {
                error = "У файлі вказаний некоректний опис";
                return false;
            }

            _context.Genres.Add(genre);
            _context.SaveChanges();
            error = "";
            return true;
        }
    }
}
