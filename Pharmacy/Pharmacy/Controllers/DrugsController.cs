using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pharmacy.Data;
using Pharmacy.Models.DTO;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;

namespace Pharmacy.Controllers
{
    public class DrugsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DrugsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Drugs
        public async Task<IActionResult> Index(string searchString = "", string filter = "", string pharmacyId = "")
        {

            List<DrugDTO> drugs;
            if (long.TryParse(pharmacyId, out long pharmacyId2))
            {
                drugs = await (from drug in _context.tbDrugs
                                join drugsQuant in _context.DrugAndQuantity on drug equals drugsQuant.Drug
                                join pharmacy in _context.tbPharmacys on drugsQuant.PharmacyId equals pharmacy.Id
                                where drugsQuant.PharmacyId == pharmacyId2 && drugsQuant.Quantity > 0
                                select new DrugDTO(drug.Id,drugsQuant.Id, drug.Name, drug.Type,drug.Form,drug.Ingredients, drug.Drugmaker,drug.IsPrescribable,drug.AverageScore,pharmacy.Name, drugsQuant.Price)).ToListAsync();
            }
            else
            {
                drugs = await (from drug in _context.tbDrugs
                               join drugsQuant in _context.DrugAndQuantity on drug equals drugsQuant.Drug
                               join pharmacy in _context.tbPharmacys on drugsQuant.PharmacyId equals pharmacy.Id
                               where drugsQuant.Quantity > 0
                               select new DrugDTO(drug.Id, drugsQuant.Id, drug.Name, drug.Type, drug.Form, drug.Ingredients, drug.Drugmaker, drug.IsPrescribable, drug.AverageScore, pharmacy.Name, drugsQuant.Price)).ToListAsync();
            }

            List<DrugDTO> filteredDrugs = new List<DrugDTO>();

            if(string.IsNullOrEmpty(searchString))
            {
                filteredDrugs = drugs;
            }
            else foreach(var drug in drugs)
            {
                var json = JsonConvert.SerializeObject(drug);
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                if (filter == "Form")
                {
                    if (drug.Form.ToString().ToUpper().Contains(searchString.ToUpper()))
                    {
                        filteredDrugs.Add(drug);
                    }
                }
                if(dictionary[filter] != null && dictionary[filter].ToUpper().Contains(searchString.ToUpper()))
                {
                    filteredDrugs.Add(drug);
                }
            }

            ViewData["PharmacyList"] = await _context.tbPharmacys.ToListAsync();
            return View(filteredDrugs);
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
        [Authorize]
        // GET: Drugs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Form,Ingredients,Drugmaker,IsPrescribable,Notes")] Drug drug)
        {
            if(drug.Id < 0)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                _context.Add(drug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(drug);
        }
        [Authorize]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Form,Ingredients,Drugmaker,IsPrescribable,Notes,AverageScore")] Drug drug)
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
        [Authorize]
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
        [Authorize]
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
