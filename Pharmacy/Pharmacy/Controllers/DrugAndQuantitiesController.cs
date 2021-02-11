using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;

namespace Pharmacy.Controllers
{
    public class DrugAndQuantitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DrugAndQuantitiesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DrugAndQuantities
        public async Task<IActionResult> Index()
        {
            ViewData["PharmacyList"] = await _context.tbPharmacys.ToListAsync();
            AppUser currentUser = await _userManager.GetUserAsync(User);

            return View(await _context.DrugAndQuantity.Include(x => x.Drug).Where(x => x.PharmacyId == currentUser.PharmacyId).ToListAsync());
        }

        // GET: DrugAndQuantities/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugAndQuantities = await _context.DrugAndQuantity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drugAndQuantities == null)
            {
                return NotFound();
            }

            return View(drugAndQuantities);
        }

        // GET: DrugAndQuantities/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DrugList"] = await _context.tbDrugs.ToListAsync();
            ViewData["PharmacyList"] = await  _context.tbPharmacys.ToListAsync();
            return View();
        }

        // POST: DrugAndQuantities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? drugId, long? quantity, long? pharmacy)
        {
            var drugAndQuantities = new DrugAndQuantities();
            drugAndQuantities.Drug = _context.tbDrugs.Find(drugId);
            drugAndQuantities.PharmacyId = (long) pharmacy;
            drugAndQuantities.Quantity = (uint) quantity;
            

            if (ModelState.IsValid)
            {
                _context.Add(drugAndQuantities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drugAndQuantities);
        }

        // GET: DrugAndQuantities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugAndQuantities = await _context.DrugAndQuantity.FindAsync(id);
            if (drugAndQuantities == null)
            {
                return NotFound();
            }
            return View(drugAndQuantities);
        }

        // POST: DrugAndQuantities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PharmacyId,Quantity, Price")] DrugAndQuantities drugAndQuantities)
        {
            if (id != drugAndQuantities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drugAndQuantities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugAndQuantitiesExists(drugAndQuantities.Id))
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
            return View(drugAndQuantities);
        }

        // GET: DrugAndQuantities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugAndQuantities = await _context.DrugAndQuantity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drugAndQuantities == null)
            {
                return NotFound();
            }

            return View(drugAndQuantities);
        }

        // POST: DrugAndQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var drugAndQuantities = await _context.DrugAndQuantity.FindAsync(id);
            _context.DrugAndQuantity.Remove(drugAndQuantities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugAndQuantitiesExists(long id)
        {
            return _context.DrugAndQuantity.Any(e => e.Id == id);
        }
    }
}
