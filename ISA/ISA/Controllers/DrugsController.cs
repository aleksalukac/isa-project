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
    public class DrugsController : Controller
    {
        private readonly Context _context;

        public DrugsController(Context context)
        {
            _context = context;
        }

        // GET: Drugs
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbDrugs.ToListAsync());
        }

        // GET: Drugs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.tbDrugs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drug == null)
            {
                return NotFound();
            }

            return View(drug);
        }

        // GET: Drugs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Form,Ingredients,Drugmaker,IsPrescribable,Notes")] Drug drug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drug);
        }

        // GET: Drugs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.tbDrugs.FindAsync(id);
            if (drug == null)
            {
                return NotFound();
            }
            return View(drug);
        }

        // POST: Drugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Form,Ingredients,Drugmaker,IsPrescribable,Notes")] Drug drug)
        {
            if (id != drug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugExists(drug.Id))
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
            return View(drug);
        }

        // GET: Drugs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drug = await _context.tbDrugs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drug == null)
            {
                return NotFound();
            }

            return View(drug);
        }

        // POST: Drugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var drug = await _context.tbDrugs.FindAsync(id);
            _context.tbDrugs.Remove(drug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugExists(long id)
        {
            return _context.tbDrugs.Any(e => e.Id == id);
        }
    }
}
