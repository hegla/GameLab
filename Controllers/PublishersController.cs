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
    public class PublishersController : Controller
    {
        private readonly gameindustryContext _context;

        public PublishersController(gameindustryContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Publishers.ToListAsync());
        }

        // GET: Publishers/Details/5
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

            return RedirectToAction("Index", "PublicForPublishers", new { id = publishers.PublisherId, name = publishers.Name });
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherId,Name,Earnings,Contacts")] Publishers publishers)
        {
            bool duplicate = await _context.Publishers.AnyAsync(l => l.Name.Equals(publishers.Name));

            if (duplicate)
            {
                ModelState.AddModelError("Name", "Введене видавництво вже додане");
            }
            if (ModelState.IsValid)
            {
                _context.Add(publishers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publishers);
        }

        // GET: Publishers/Edit/5
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

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId,Name,Earnings,Contacts")] Publishers publishers)
        {
            bool duplicate = await _context.Publishers.AnyAsync(l => l.Name.Equals(publishers.Name) && !l.PublisherId.Equals(publishers.PublisherId));

            if (duplicate)
            {
                ModelState.AddModelError("Name", "Введене видавництво вже додане");
            }
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

        // GET: Publishers/Delete/5
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

        // POST: Publishers/Delete/5
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

            return RedirectToAction("Index", "Publishers");
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
            Publishers publisher = new Publishers();
            string name = row.Cell(1).Value.ToString();
            string earnings = row.Cell(2).Value.ToString();
            string email = row.Cell(3).Value.ToString();

            string regexName = @"^[А-ЯІЇЄа-яіїєA-Za-z'-'' ']*$";
            string regexEarn = @"^[1-9][0-9]*$";
            string regexCont = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (Regex.IsMatch(name, regexName, RegexOptions.IgnoreCase))
            {
                bool dublicate = _context.Publishers.Any(l => l.Name.Equals(name));
                if(dublicate)
                {
                    error = "Наявна існуюча в базі назва";
                    return false;
                } 
                else
                {
                    publisher.Name = name;
                }
            }
            else
            {
                error = "У файлі наявне некоректна назва";
                return false;
            }

            if (Regex.IsMatch(earnings, regexEarn, RegexOptions.IgnoreCase))
            {
                publisher.Earnings = Convert.ToDouble(earnings);
            }
            else
            {
                error = "У файлі наявний некоректний дохід";
                return false;
            }

            if (Regex.IsMatch(email, regexCont, RegexOptions.IgnoreCase))
            {
                publisher.Contacts = email;
            }
            else
            {
                error = "У файлі вказана некоректна електронна адреса";
                return false;
            }
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            error = "";
            return true;
        }
    }
}
