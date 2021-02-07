using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;

namespace Pharmacy.Controllers
{
    public class RatingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RatingsController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [Authorize]
        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            return View(await _context.Rating
                .Include(x => x.Drug)
                .Include(x => x.Employee)
                .Include(x => x.Pharmacy)
                .Include(x => x.User)
                .Where(x => x.User.Id == loggedInUser.Id)
                .ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }
        [Authorize]
        // GET: Ratings/CreateEmployee
        public async Task<IActionResult> CreateEmployee()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            var employee = await _context.tbAppointments.Where(x => x.PatientID == loggedInUser.Id).Select(x => x.MedicalExpertID).ToListAsync();

            ViewData["EmployeeList"] = await _context.AppUsers.Where(x => employee.Contains(x.Id)).ToListAsync();

            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(string? employeeId, int? score)
        {
            if(employeeId != null && score != null)
            {
                //new rating
                Rating rating = new Rating();
                rating.Employee = await _context.AppUsers.FindAsync(employeeId);
                rating.Score = (int)score;
                rating.User = await _userManager.GetUserAsync(User);
                _context.Add(rating);
                await _context.SaveChangesAsync();

                //modify employee
                rating.Employee.AverageScore = _context.Rating.Where(m => m.Employee.Id == rating.Employee.Id).Select(m => (float)m.Score).Average();
                _context.Update(rating.Employee);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(CreateEmployee));
            }
        }


        [Authorize]
        // GET: Ratings/CreateEmployee
        public async Task<IActionResult> CreatePharmacy()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            List<long> pharmacyOrders = await _context.tbOrders
                .Include(x => x.User)
                .Include(x => x.DrugAndQuantities)
                .Where(x => x.TransactionComplete && x.User.Id == loggedInUser.Id)
                .Select(x => x.DrugAndQuantities.PharmacyId)
                .ToListAsync();

            List<long> pharmacyAppoitments = await _context.tbAppointments
                .Where(x => x.PatientID == loggedInUser.Id)
                .Select(x => x.PhrmacyId)
                .ToListAsync();

            pharmacyOrders.AddRange(pharmacyAppoitments);
            List<long> allPharmacy = (List<long>) pharmacyOrders;

            ViewData["PharmacyList"] = await _context.tbPharmacys.Where(x => allPharmacy.Contains(x.Id)).ToListAsync();

            return View();
        }

        // POST: Ratings/CreatePharmacy
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePharmacy(long? pharmacyId, int? score)
        {
            if (pharmacyId != null && score != null)
            {
                //new rating
                Rating rating = new Rating();
                rating.Pharmacy = await _context.tbPharmacys.FindAsync(pharmacyId);
                rating.Score = (int)score;
                rating.User = await _userManager.GetUserAsync(User);
                _context.Add(rating);
                await _context.SaveChangesAsync();

                //modify employee
                rating.Pharmacy.AverageScore = _context.Rating.Where(m => m.Pharmacy.Id == rating.Pharmacy.Id).Select(m => (float)m.Score).Average();
                _context.Update(rating.Pharmacy);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(CreatePharmacy));
            }
        }


        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Score")] Rating rating)
        {
            if (id != rating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Id))
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
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rating = await _context.Rating.FindAsync(id);
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(long id)
        {
            return _context.Rating.Any(e => e.Id == id);
        }
    }
}
