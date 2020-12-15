using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISA.Models;
using ISA.Models.Entities;

namespace ISA.Controllers
{
    public class DrugAndQuantitiesController : Controller
    {
        private readonly Context _context;

        public DrugAndQuantitiesController(Context context)
        {
            _context = context;
        }

        // GET: DrugAndQuantities
        public async Task<IActionResult> Index()
        {
            return View(await _context.DrugAndQuantity.ToListAsync());
        }

        // GET: DrugAndQuantities/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugAndQuantity = await _context.DrugAndQuantity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drugAndQuantity == null)
            {
                return NotFound();
            }

            return View(drugAndQuantity);
        }

        // GET: DrugAndQuantities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DrugAndQuantities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity")] DrugAndQuantity drugAndQuantity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drugAndQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drugAndQuantity);
        }

        // GET: DrugAndQuantities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugAndQuantity = await _context.DrugAndQuantity.FindAsync(id);
            if (drugAndQuantity == null)
            {
                return NotFound();
            }
            return View(drugAndQuantity);
        }

        // POST: DrugAndQuantities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Quantity")] DrugAndQuantity drugAndQuantity)
        {
            if (id != drugAndQuantity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drugAndQuantity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugAndQuantityExists(drugAndQuantity.Id))
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
            return View(drugAndQuantity);
        }

        // GET: DrugAndQuantities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugAndQuantity = await _context.DrugAndQuantity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drugAndQuantity == null)
            {
                return NotFound();
            }

            return View(drugAndQuantity);
        }

        // POST: DrugAndQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var drugAndQuantity = await _context.DrugAndQuantity.FindAsync(id);
            _context.DrugAndQuantity.Remove(drugAndQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugAndQuantityExists(long id)
        {
            return _context.DrugAndQuantity.Any(e => e.Id == id);
        }
    }
}
