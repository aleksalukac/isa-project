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
    public class SupplyOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplyOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SupplyOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbSupplyOrders.ToListAsync());
        }

        // GET: SupplyOrders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyOrder = await _context.tbSupplyOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyOrder == null)
            {
                return NotFound();
            }

            return View(supplyOrder);
        }

        // GET: SupplyOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SupplyOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PharmacyId,DeleveryDate,DrugId,ExtraQuantity")] SupplyOrder supplyOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplyOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplyOrder);
        }

        // GET: SupplyOrders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyOrder = await _context.tbSupplyOrders.FindAsync(id);
            if (supplyOrder == null)
            {
                return NotFound();
            }
            return View(supplyOrder);
        }

        // POST: SupplyOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PharmacyId,DeleveryDate,DrugId,ExtraQuantity")] SupplyOrder supplyOrder)
        {
            if (id != supplyOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplyOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplyOrderExists(supplyOrder.Id))
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
            return View(supplyOrder);
        }

        // GET: SupplyOrders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyOrder = await _context.tbSupplyOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyOrder == null)
            {
                return NotFound();
            }

            return View(supplyOrder);
        }

        // POST: SupplyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var supplyOrder = await _context.tbSupplyOrders.FindAsync(id);
            _context.tbSupplyOrders.Remove(supplyOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplyOrderExists(long id)
        {
            return _context.tbSupplyOrders.Any(e => e.Id == id);
        }
    }
}
