
using TwoFactorAuth.Models;
using TwoFactorAuth.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuth.Controllers
{
    public class CellPhonesController : Controller
    {
        private readonly AppDbContext _context;

        public CellPhonesController(AppDbContext context)
        {
            _context = context;
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            ViewBag.Name = "Items";
            CellPhoneViewModel CPvm = new CellPhoneViewModel();
            CPvm.CellPhones = await _context.CellPhones.ToListAsync();

            return View(CPvm);

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cellPhone = await _context.CellPhones
                .FirstOrDefaultAsync(m => m.CellPhoneId == id);
            if (cellPhone == null)
            {
                return NotFound();
            }

            return View(cellPhone);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var allCategories = _context.Categories.ToList();
            if (allCategories != null)
            {
                ViewBag.allCategories = allCategories;
            }
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CellPhoneId,CellPhoneName,ShortDescription,LongDescription,Image,Price,InStock,CategoryId")] CellPhone cellPhone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cellPhone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cellPhone);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cellPhone = await _context.CellPhones.FindAsync(id);
            if (cellPhone == null)
            {
                return NotFound();
            }
            return View(cellPhone);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CellPhoneId,CellPhoneName,ShortDescription,LongDescription,Image,Price,InStock")] CellPhone cellPhone)
        {
            if (id != cellPhone.CellPhoneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cellPhone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(cellPhone.CellPhoneId))
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
            return View(cellPhone);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cellPhone = await _context.CellPhones
                .FirstOrDefaultAsync(m => m.CellPhoneId == id);
            if (cellPhone == null)
            {
                return NotFound();
            }

            return View(cellPhone);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cellPhone = await _context.CellPhones.FindAsync(id);
            _context.CellPhones.Remove(cellPhone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
