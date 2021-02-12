using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Services;

namespace Pharmacy.Controllers
{
    public class PharmaciesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPharmacyService _pharmacyService;
        private readonly IAppointmentService _appointmentService;
        private readonly IEmailSender _emailService;

        public PharmaciesController(UserManager<AppUser> userManager,
            ApplicationDbContext context,
            IPharmacyService pharmacyService,
            IAppointmentService appointmentService,
            IEmailSender emailSender)
        {
            _emailService = emailSender;
            _appointmentService = appointmentService;
            _pharmacyService = pharmacyService;
            _userManager = userManager;
            _context = context;
        }

        // GET: Pharmacies LoggedInIndex
        public async Task<IActionResult> Index(string searchString = "", string filter = "", string sort = "")
        {
            return View(await _pharmacyService.GetAllFiltered(searchString,filter,sort));
        }

        [Authorize]
        public async Task<IActionResult> LoggedInIndex(string searchString = "", string filter = "", string sort = "")
        {
            ViewData["PharmacyId"] = (await _userManager.GetUserAsync(User)).Id;
            return View(await _pharmacyService.GetAllFiltered(searchString, filter, sort));
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

        [Authorize(Roles ="User")]
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

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UnSubscribe(long? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userSubcription = await _context.UserSubscribed.FirstOrDefaultAsync(x => x.PharmacyId == id && x.UserId == user.Id);
            _context.Remove(userSubcription);
            await _context.SaveChangesAsync();

            return View();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> SubscribedPharmacy()
        {
            var user = await _userManager.GetUserAsync(User);
            var userSubcription = await _context.UserSubscribed.Where(x => x.UserId == user.Id).Select(x => x.PharmacyId).ToListAsync();
            return View(await _context.tbPharmacys.Where(x => userSubcription.Contains(x.Id)).ToListAsync());
        }
        public IActionResult GetAvailablePharmacies()
        {
            ViewData["Cost"] = 40;
            return View(new List<Models.Entities.Pharmacy>());
        }
        [HttpPost]
        public async Task<IActionResult> GetAvailablePharmacies(DateTime dateTime)
        {
            ViewData["Cost"] = 40;
            ViewData["DateTime"] = dateTime;
            return View(await _pharmacyService.GetAvailablePharmacies(dateTime));
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleAppointment(DateTime dateTime,long pharmacyId)
        {
            ViewData["Cost"] = 40;
            ViewData["DateTime"] = dateTime;
            var pharmacy = await _pharmacyService.GetById(pharmacyId);
            ViewData["PharmacyId"] = pharmacyId;
            ViewData["PharmacyName"] = pharmacy.Name;
            ViewData["Pharmacists"] = await _pharmacyService.GetAllPharmacists(pharmacyId, dateTime);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TakeAppointment(DateTime dateTime, long pharmacyId, double cost,string pharmacistsd)
        {
            var user = await _userManager.GetUserAsync(User);
            Appointment appointment = new Appointment();
            appointment.PhrmacyId = pharmacyId;
            appointment.Duration = TimeSpan.FromMinutes(30);
            appointment.MedicalExpertID = pharmacistsd;
            appointment.PatientID = user.Id;
            appointment.Price = (float)cost;
            appointment.StartDateTime = dateTime;
            appointment.Type = AppointmentType.Counseling;

            try
            {
                _context.tbAppointments.Add(appointment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appointmentService.Exists(appointment.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View("ConcurrencyError", "Home");
                }
            }
            await _emailService.SendEmailAsync(user.Email, "Scheduled Appointment",
                $"Scheduled Appointment for {user.FirstName} at {dateTime}");

            return View();
        }
        
    }
}
