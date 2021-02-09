using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;

namespace Pharmacy.Controllers
{
    public class PharmaciesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PharmaciesController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Pharmacies
        public async Task<IActionResult> Index(string searchString = "", string filter = "", string sort = "")
        {
            List<Models.Entities.Pharmacy> pharmacies = new;

            if (sort != null)
            {
                pharmacies = await _context.tbPharmacys.ToListAsync();
                if (sort == "Score")
                {
                    pharmacies = await _context.tbPharmacys.OrderBy(x => x.AverageScore).ToListAsync();
                } else if (sort == "Name")
                {
                    pharmacies = await _context.tbPharmacys.OrderBy(x => x.Name).ToListAsync();
                } else if (sort == "Adress")
                {
                    pharmacies = await _context.tbPharmacys.OrderBy(x => x.Address).ToListAsync();
                }
            }

            ViewData["PharmacyId"] = (await _userManager.GetUserAsync(User)).Id;
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

            return View(FilterPharmacies(filteredPharmacies, searchString, filter));
        }

        private static List<Pharmacy.Models.Entities.Pharmacy> FilterPharmacies(List<Pharmacy.Models.Entities.Pharmacy> pharmacies, string searchString, string filter)
        {
            List<Pharmacy.Models.Entities.Pharmacy> filteredUsers = new List<Pharmacy.Models.Entities.Pharmacy>();
            if (string.IsNullOrEmpty(searchString))
            {
                filteredUsers = pharmacies;
            }
            else
            {
                foreach (var user in pharmacies)
                {
                    var json = JsonConvert.SerializeObject(user);
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (dictionary[filter] != null && dictionary[filter].ToUpper().Contains(searchString.ToUpper()))
                    {
                        filteredUsers.Add(user);
                    }
                }
            }

            return filteredUsers;
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Address,Name")] Pharmacy.Models.Entities.Pharmacy pharmacy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacy);
        }

        [Authorize(Roles = "PharmacyAdmin,Admin")]
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
        [Authorize(Roles = "PharmacyAdmin,Admin")]
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
                    var pharmacytmp = await _context.tbPharmacys.FindAsync(id);
                    pharmacytmp.Name = pharmacy.Name;
                    pharmacytmp.Address = pharmacy.Address;
                    _context.Update(pharmacytmp);
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
        [Authorize(Roles = "PharmacyAdmin,Admin")]
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
        [Authorize(Roles = "PharmacyAdmin,Admin")]
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

        public async Task<IActionResult> Subscribe(long? id)
        {
            var user = await _userManager.GetUserAsync(User);
            UserSubscribed userSubscribed = new UserSubscribed();
            userSubscribed.PharmacyId = (long) id;
            userSubscribed.UserId =  user.Id;
            _context.Add(userSubscribed);
            await _context.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> UnSubscribe(long? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userSubcription = await _context.UserSubscribed.FirstOrDefaultAsync(x => x.PharmacyId == id && x.UserId == user.Id);
            _context.Remove(userSubcription);
            await _context.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> SubscribedPharmacy()
        {
            var user = await _userManager.GetUserAsync(User);
            var userSubcription = await _context.UserSubscribed.Where(x => x.UserId == user.Id).Select(x => x.PharmacyId).ToListAsync();
            return View(await _context.tbPharmacys.Where(x => userSubcription.Contains(x.Id)).ToListAsync());
        }
    }
}
