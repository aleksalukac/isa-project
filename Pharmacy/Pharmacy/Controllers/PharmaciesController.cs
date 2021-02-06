using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pharmacy.Data;
using Pharmacy.Models.Entities;

namespace Pharmacy.Controllers
{
    public class PharmaciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PharmaciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pharmacies
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString = "", string filter = "")
        {
            var pharmacies = await _context.tbPharmacys.ToListAsync();
            List<Pharmacy.Models.Entities.Pharmacy> filteredPharmacies = new List<Pharmacy.Models.Entities.Pharmacy>();

            if (searchString == null || searchString.Length == 0)
            {
                filteredPharmacies = pharmacies;
            }
            else
            {
                foreach (var pharmacy in pharmacies)
                {
                    var json = JsonConvert.SerializeObject(pharmacy);
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (dictionary[filter] != null && dictionary[filter].ToUpper().Contains(searchString.ToUpper()))
                    {
                        filteredPharmacies.Add(pharmacy);
                    }
                }
            }

            return View(filteredPharmacies);
        }

        // GET: Pharmacies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacy = await _context.tbPharmacys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pharmacy == null)
            {
                return NotFound();
            }

            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            ViewData["Address"] = rgx.Replace(pharmacy.Address, "").Replace(" ", "%20");
            return View(pharmacy);
        }

        // GET: Pharmacies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pharmacies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address")] Pharmacy.Models.Entities.Pharmacy pharmacy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacy);
        }

        // GET: Pharmacies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacy = await _context.tbPharmacys.FindAsync(id);
            if (pharmacy == null)
            {
                return NotFound();
            }
            return View(pharmacy);
        }

        // POST: Pharmacies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Address")] Pharmacy.Models.Entities.Pharmacy pharmacy)
        {
            if (id != pharmacy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacyExists(pharmacy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Home");
            }
            return View(pharmacy);
        }

        // GET: Pharmacies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacy = await _context.tbPharmacys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pharmacy == null)
            {
                return NotFound();
            }

            return View(pharmacy);
        }

        // POST: Pharmacies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var pharmacy = await _context.tbPharmacys.FindAsync(id);
            _context.tbPharmacys.Remove(pharmacy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacyExists(long id)
        {
            return _context.tbPharmacys.Any(e => e.Id == id);
        }
    }
}
