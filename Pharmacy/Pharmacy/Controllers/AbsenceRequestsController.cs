﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pharmacy.Areas.Identity;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Services;

namespace Pharmacy.Controllers
{
    public class AbsenceRequestsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<Pharmacy.Models.Entities.Users.AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly EmailSender _emailSender;
        private readonly IUserService _userService;
        private readonly IPharmacyService _pharmacyService;
        private readonly IAbsenceRequestService _absenceRequestService;

        public AbsenceRequestsController(ApplicationDbContext context, UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager, IUserService userService, IAbsenceRequestService absenceRequestService,
                IEmailSender emailSender, IPharmacyService pharmacyService)
        {
            _context = context; 
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _pharmacyService = pharmacyService;
            _absenceRequestService = absenceRequestService;

            using (StreamReader r = new StreamReader("./Areas/Identity/emailCredentials.json"))
            {
                string json = r.ReadToEnd();
                _emailSender = JsonConvert.DeserializeObject<EmailSender>(json);
            }
        }

        // GET: AbsenceRequests
        [Authorize(Roles = "Dermatologist,Pharmacist,PharmacyAdmin")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            List<AppUser> appUsers = await _context.AppUsers.ToListAsync();
            ViewData["EmployeeList"] = appUsers;
            var userAll = await _userService.GetUserRole(user.Id);
            if (userAll.Equals("Pharmacist") || userAll.Equals("Dermatologist"))
            {
                return View(await _absenceRequestService.GetByUser(user.Id));
            }
            var threadAbsence = await _context.tbAbsenceRequests.ToListAsync();
            return View(threadAbsence);
        }

        // GET: AbsenceRequests/Details/5
        [Authorize(Roles = "Dermatologist,Pharmacist,PharmacyAdmin")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceRequest = await _absenceRequestService.GetById(id.Value);

            if (absenceRequest == null)
            {
                return NotFound();
            }

            return View(absenceRequest);
        }

        // GET: AbsenceRequests/Create
        [Authorize(Roles = "Dermatologist,Pharmacist")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: AbsenceRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Dermatologist,Pharmacist")]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,StartDateTime,EndDateTime,PharmacyAdministratorId")] AbsenceRequest absenceRequest)
        {
            var userName = await _userManager.GetUserAsync(User);

            absenceRequest.EmployeeId = userName.Id;

            Pharmacy.Models.Entities.Pharmacy pharmacy = await _pharmacyService.GetById(userName.PharmacyId);
            AppUser pharmacyAdmin = await _userService.GetById(pharmacy.AdminUserID);

            absenceRequest.PharmacyAdministratorId = pharmacyAdmin.Id;

            if (ModelState.IsValid)
            {
                _ = await _context.AddAsync(absenceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(absenceRequest);
        }

        // GET: AbsenceRequests/Edit/5
        [Authorize(Roles = "Dermatologist,Pharmacist,PharmacyAdmin")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceRequest = await _context.tbAbsenceRequests.FindAsync(id);
            if (absenceRequest == null)
            {
                return NotFound();
            }
            return View(absenceRequest);
        }

        // POST: AbsenceRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Dermatologist,Pharmacist,PharmacyAdmin")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,EmployeeId,StartDateTime,EndDateTime,PharmacyAdministratorId")] AbsenceRequest absenceRequest)
        {
            if (id != absenceRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absenceRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceRequestExists(absenceRequest.Id))
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
            return View(absenceRequest);
        }

        // GET: AbsenceRequests/Delete/5
        [Authorize(Roles = "Dermatologist,Pharmacist,PharmacyAdmin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceRequest = await _context.tbAbsenceRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absenceRequest == null)
            {
                return NotFound();
            }

            return View(absenceRequest);
        }

        // POST: AbsenceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var absenceRequest = await _context.tbAbsenceRequests.FindAsync(id);
            _context.tbAbsenceRequests.Remove(absenceRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsenceRequestExists(long id)
        {
            return _context.tbAbsenceRequests.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CheckingForApproveal()
        {
            List<AppUser> appUsers = await _context.AppUsers.ToListAsync();
            ViewData["EmployeeList"] = appUsers;
            var userName = await _userManager.GetUserAsync(User);
            var listOfUnApproved = await _context.tbAbsenceRequests.Where(x => x.Approved == false && x.PharmacyAdministratorId == userName.Id).ToListAsync();
            return View(listOfUnApproved);
        }

        public async Task<IActionResult> Approve(long id)
        {

            var absenceRequest = await _context.tbAbsenceRequests.FindAsync(id);
            absenceRequest.Approved = true;

                try
                {
                    _context.Update(absenceRequest);

                    var user = await _userManager.GetUserAsync(User);
                    await _emailSender.SendEmailAsync(user.Email, "Absence Requiest Respons of Admin",
                        $"Your absence Requiest has been approved");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceRequestExists(absenceRequest.Id))
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

        public async Task<IActionResult> Reject(long id)
        {
            var absenceRequest = _context.tbAbsenceRequests.Find(id);
            absenceRequest.Approved = false;

            try
            {
                _context.Update(absenceRequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbsenceRequestExists(absenceRequest.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("RejectText", "AbsenceRequests", new { id = absenceRequest.Id});
        }

        // AbsenceRequestsController/RejectText/3
        
        [HttpGet]
        public async Task<IActionResult> RejectText(long id)
        {
            var absenceRequest = _context.tbAbsenceRequests.Find(id);
            absenceRequest.Approved = false;
            
            try
            {
                _context.tbAbsenceRequests.Remove(absenceRequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbsenceRequestExists(absenceRequest.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RejectText(string configname)
        {
            var user = await _userManager.GetUserAsync(User);
            await _emailSender.SendEmailAsync(user.Email, "Absence Requiest Respons of admin :",
                $" "+ configname);
            return RedirectToAction(nameof(Index));
        }

    }
}
