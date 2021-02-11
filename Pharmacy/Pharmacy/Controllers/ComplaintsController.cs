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
using Pharmacy.Services;

namespace Pharmacy.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IComplaintService _complaintService;

        public ComplaintsController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Complaints
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Complaint.ToListAsync());
        }

        // GET: Complaints/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        [Authorize]
        // GET: Complaints/Create
        public async Task<IActionResult> CreateStaff()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            var employee = await _context.tbAppointments.Where(x => x.PatientID == loggedInUser.Id).Select(x => x.MedicalExpertID).ToListAsync();

            ViewData["EmployeeList"] = await _context.AppUsers.Where(x => employee.Contains(x.Id)).ToListAsync();
            return View();
        }
        [Authorize]
        // POST: Complaints/CreateStaff
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStaff(string employeeId, string reportText)
        {
            var complaint = new Complaint();
            if (employeeId != null && reportText != null)
            {
                complaint.ReportText = reportText;
                complaint.Employee = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == employeeId);
                complaint.User = await _userManager.GetUserAsync(User);

                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ConfirmComplaint));
            }

            return View(complaint);
        }
        [Authorize]
        public IActionResult ConfirmComplaint()
        {
            return View();
        }
        [Authorize]
        // Get: Complaints/CreatePharmacy
        public async Task<IActionResult> CreatePharmacy()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            List<long> pharmacyOrders = await _context.tbOrders
                .Include(x => x.DrugAndQuantities)
                .Where(x => x.TransactionComplete && x.UserId == loggedInUser.Id)
                .Select(x => x.DrugAndQuantities.PharmacyId)
                .ToListAsync();

            List<long> pharmacyAppoitments = await _context.tbAppointments
                .Where(x => x.PatientID == loggedInUser.Id)
                .Select(x => x.PhrmacyId)
                .ToListAsync();
            pharmacyOrders.AddRange(pharmacyAppoitments);
            
            ViewData["PharmacyList"] = await _context.tbPharmacys.Where(x => pharmacyOrders.Contains(x.Id)).ToListAsync();
            return View();
        }
        [Authorize]
        // POST: Complaints/CreatePharmacy
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePharmacy(long? pharmacyId, string reportText)
        {

            var complaint = new Complaint();
            if (pharmacyId != null && reportText != null)
            {
                complaint.ReportText = reportText;
                complaint.Pharmacy = await _context.tbPharmacys.FirstOrDefaultAsync(x => x.Id == pharmacyId);
                complaint.User = await _userManager.GetUserAsync(User);

                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ConfirmComplaint));
            }

            return View(complaint);
        }
        [Authorize]
        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            return View(complaint);
        }
        [Authorize]
        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ReportText")] Complaint complaint)
        {
            if (id != complaint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return View("ConcurrencyError", "Home");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var complaint = await _context.Complaint.FindAsync(id);
            _context.Complaint.Remove(complaint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(long id)
        {
            return _context.Complaint.Any(e => e.Id == id);
        }
    }
}
