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
    public class DrugTypesController : Controller
    {
        private readonly Context _context;

        public DrugTypesController(Context context)
        {
            _context = context;
        }

        // GET: DrugTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DrugType.ToListAsync());
        }

        // GET: DrugTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drugType == null)
            {
                return NotFound();
            }

            return View(drugType);
        }

        // GET: DrugTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DrugTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] DrugType drugType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drugType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drugType);
        }

        // GET: DrugTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugType.FindAsync(id);
            if (drugType == null)
            {
                return NotFound();
            }
            return View(drugType);
        }

        // POST: DrugTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id")] DrugType drugType)
        {
            if (id != drugType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drugType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugTypeExists(drugType.Id))
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
            return View(drugType);
        }

        // GET: DrugTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drugType == null)
            {
                return NotFound();
            }

            return View(drugType);
        }

        // POST: DrugTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var drugType = await _context.DrugType.FindAsync(id);
            _context.DrugType.Remove(drugType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugTypeExists(long id)
        {
            return _context.DrugType.Any(e => e.Id == id);
        }
    }
}
