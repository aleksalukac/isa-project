using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;

namespace Pharmacy.Controllers
{
    [Authorize(Roles = "PharmacyAdmin")]
    public class SupplyItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplyItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SupplyItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.SupplyItems.ToListAsync());
        }

        // GET: SupplyItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyItem = await _context.SupplyItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyItem == null)
            {
                return NotFound();
            }

            return View(supplyItem);
        }

        // GET: SupplyItems/Create
        public async Task<IActionResult> CreateAsync(SupplyItem supply)
        {
            ViewData["DrugList"] = await _context.tbDrugs.ToListAsync();
            return View();
        }

        // POST: SupplyItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DrugId,SupplyOrderId,ExtraQuantity")] SupplyItem supplyItem)
        {
            if(supplyItem.ExtraQuantity < 0 || supplyItem.Id <0)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                await _context.AddAsync(supplyItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplyItem);
        }

        // GET: SupplyItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyItem = await _context.SupplyItems.FindAsync(id);
            if (supplyItem == null)
            {
                return NotFound();
            }
            return View(supplyItem);
        }

        // POST: SupplyItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DrugId,SupplyOrderId,ExtraQuantity")] SupplyItem supplyItem)
        {
            if (id != supplyItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplyItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplyItemExists(supplyItem.Id))
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
            return View(supplyItem);
        }

        // GET: SupplyItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyItem = await _context.SupplyItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyItem == null)
            {
                return NotFound();
            }

            return View(supplyItem);
        }

        // POST: SupplyItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            _context.SupplyItems.Remove(_context.SupplyItems.Find(id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplyItemExists(long id)
        {
            return _context.SupplyItems.Any(e => e.Id == id);
        }
    }
}
