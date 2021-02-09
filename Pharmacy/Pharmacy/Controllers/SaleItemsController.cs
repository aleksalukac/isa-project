using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;

namespace Pharmacy.Controllers
{
    public class SaleItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaleItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SaleItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleItems.ToListAsync());
        }

        // GET: SaleItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleItems = await _context.SaleItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleItems == null)
            {
                return NotFound();
            }

            return View(saleItems);
        }

        // GET: SaleItems/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["DrugAndQuantities"] = await  _context.DrugAndQuantity.ToListAsync();
            ViewData["Drugs"] = await _context.tbDrugs.ToListAsync();
            ViewData["SaleItems"] = await _context.SaleItems.ToListAsync();
            return View();
        }

        // POST: SaleItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BeforePrice,EndTime, DrugAndQuantitiesId")] SaleItems saleItems, long DrugAndQuantitiesId)
        {
            if (ModelState.IsValid)
            {
                saleItems.DrugAndQuantitiesId = DrugAndQuantitiesId;
                _context.Add(saleItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleItems);
        }

        // GET: SaleItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleItems = await _context.SaleItems.FindAsync(id);
            if (saleItems == null)
            {
                return NotFound();
            }
            return View(saleItems);
        }

        // POST: SaleItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,BeforePrice,EndTime")] SaleItems saleItems)
        {
            if (id != saleItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleItemsExists(saleItems.Id))
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
            return View(saleItems);
        }

        // GET: SaleItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleItems = await _context.SaleItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleItems == null)
            {
                return NotFound();
            }

            return View(saleItems);
        }

        // POST: SaleItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var saleItems = await _context.SaleItems.FindAsync(id);
            _context.SaleItems.Remove(saleItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleItemsExists(long id)
        {
            return _context.SaleItems.Any(e => e.Id == id);
        }
    }
}
