﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pharmacy.Data;
using Pharmacy.Models;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;

        public AppUsersController(ApplicationDbContext context, UserManager<AppUser> userManager,
                                    IUserService userService, IAppointmentService appointmentService)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
            _appointmentService = appointmentService;
        }

        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.tbAppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            List<string> userRole = await (from userrole in _context.UserRoles
                                             join role in _context.Roles on userrole.RoleId equals role.Id
                                             where userrole.UserId == id
                                             select role.Name).ToListAsync();
            ViewData["roleName"] = userRole.Count == 0 ? "" : userRole[0];
            ViewData["pharmacyId"] = appUser.PharmacyId;
            
            return View(appUser);
        }

        // GET: AppUsers/UserList
        public async Task<IActionResult> UserList()
        {
            List<string> entryPoint = await (from userrole in _context.UserRoles
                                             join role in _context.Roles on userrole.RoleId equals role.Id
                                             where role.Name == "User"
                                             select userrole.UserId).ToListAsync();

            ViewData["roleName"] = "User";
            return View(await _context.AppUsers.Where(e => entryPoint.Contains(e.Id)).ToListAsync());
        }

        // GET: AppUsers/PharmacyAdminList
        [Authorize(Roles = "Dermatologist,Pharmacist")]
        public async Task<IActionResult> MyPatients(string searchString = "", string filter = "", string pharmacy = "")
        {
            var user = await _userManager.GetUserAsync(User);

            var appointmentsByMedicalExpert = _appointmentService.GetByMedicalExpert(user.Id).Result;

            var patientIds = appointmentsByMedicalExpert.Select(x => x.PatientID);
            patientIds = (new HashSet<string>(patientIds)).ToList();

            var patients = await _userService.GetByList(patientIds.ToList());

            return View(FilterUsers(patients, searchString, filter));
        }

        // GET: AppUsers/PharmacyAdminList
        public async Task<IActionResult> PharmacyAdminList()
        {
            List<string> entryPoint = await (from userrole in _context.UserRoles
                                             join role in _context.Roles on userrole.RoleId equals role.Id
                                             where role.Name == "PharmacyAdmin"
                                             select userrole.UserId).ToListAsync();

            ViewData["roleName"] = "User";
            return View(await _context.AppUsers.Where(e => entryPoint.Contains(e.Id)).ToListAsync());
        }

        // GET: AppUsers/PharmacistList/
        public async Task<IActionResult> PharmacistList(string searchString = "", string filter = "", string pharmacy = "")
        {
            List<string> entryPoint = await (from userrole in _context.UserRoles
                                             join role in _context.Roles on userrole.RoleId equals role.Id
                                             where role.Name == "Pharmacist"
                                             select userrole.UserId).ToListAsync();

            List<AppUser> users;
            if (long.TryParse(pharmacy, out long pharmacyId))
            {
                users = await _context.AppUsers.Where(e => entryPoint.Contains(e.Id) && e.PharmacyId == pharmacyId).ToListAsync();
            }
            else
            {
                users = await _context.AppUsers.Where(e => entryPoint.Contains(e.Id)).ToListAsync();
            }

            ViewData["roleName"] = "Pharmacist";
            ViewData["PharmacyList"] = await _context.tbPharmacys.ToListAsync();
            return View(FilterUsers(users, searchString, filter));
        }

        // GET: AppUsers/AdminList
        public async Task<IActionResult> AdminList()
        {
            List<string> entryPoint = await (from userrole in _context.UserRoles
                                             join role in _context.Roles on userrole.RoleId equals role.Id
                                             where role.Name == "Admin"
                                             select userrole.UserId).ToListAsync();

            ViewData["roleName"] = "User";
            return View(await _context.AppUsers.Where(e => entryPoint.Contains(e.Id)).ToListAsync());
        }

        // GET: AppUsers/DermatologistList
        public async Task<IActionResult> DermatologistList(string searchString = "", string filter = "", string pharmacy = "")
        {
            List<string> entryPoint = await (from userrole in _context.UserRoles
                                             join role in _context.Roles on userrole.RoleId equals role.Id
                                             where role.Name == "Dermatologist"
                                             select userrole.UserId).ToListAsync();

            List<AppUser> users;
            if (long.TryParse(pharmacy, out long pharmacyId))
            {
                users = await _context.AppUsers.Where(e => entryPoint.Contains(e.Id) && e.PharmacyId == pharmacyId).ToListAsync();
            }
            else
            {
                users = await _context.AppUsers.Where(e => entryPoint.Contains(e.Id)).ToListAsync();
            }

            ViewData["roleName"] = "Dermatologist";
            ViewData["PharmacyList"] = await _context.tbPharmacys.ToListAsync();
            return View(FilterUsers(users,searchString,filter));
        }

        [HttpGet("AppUsers/Edit/{id}")]
        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.tbAppUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: Drugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("AppUsers/Edit/{id}")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Address,Country,City,Penalty,AverageScore")] AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 try
                {
                    var repositoryUser = await _userService.GetById(id);

                    repositoryUser.FirstName = appUser.FirstName;
                    repositoryUser.LastName = appUser.LastName;
                    repositoryUser.Address = appUser.Address;
                    repositoryUser.City = appUser.City;

                    await _userService.Update(repositoryUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_userService.Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return View("ConcurrencyError", "Home");
                    }
                }
                return RedirectToAction("UserList");
            }
            return View(appUser);
        }

        [HttpGet("AppUsers/Delete/{id}")]
        // GET: Drugs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.tbAppUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: Drugs/Delete/5
        [HttpPost("AppUsers/Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appUser = await _context.tbAppUsers.FindAsync(id);
            _context.tbAppUsers.Remove(appUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserList");
        }

        protected bool AppUserExists(string id)
        {
            return _context.tbAppUsers.Any(e => e.Id == id);
        }

        private static List<AppUser> FilterUsers(List<AppUser> users, string searchString, string filter)
        {
            List<AppUser> filteredUsers = new List<AppUser>();
            if (string.IsNullOrEmpty(searchString))
            {
                filteredUsers = users;
            }
            else
            {
                foreach (var user in users)
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
    }
}
