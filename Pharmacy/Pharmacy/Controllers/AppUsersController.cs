using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pharmacy.Data;
using Pharmacy.Models;
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

        public AppUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("AppUsers/Details/{roleName}")]
        // GET: Appointments
        public async Task<IActionResult> Details(string roleName = "User")
        {
            List<string> entryPoint = await (from userrole in _context.UserRoles
                              join role in _context.Roles on userrole.RoleId equals role.Id
                              where role.Name == roleName
                              select userrole.UserId).ToListAsync();

            ViewData["roleName"] = roleName;
            return View(await _context.AppUsers.Where(e => entryPoint.Contains(e.Id)).ToListAsync());
        }

        [HttpGet("AppUsers/UserList")]
        // GET: Appointments
        public new async Task<IActionResult> UserList()
        {
            List<string> entryPoint = await (from userrole in _context.UserRoles
                                             join role in _context.Roles on userrole.RoleId equals role.Id
                                             where role.Name == "User"
                                             select userrole.UserId).ToListAsync();

            ViewData["roleName"] = "User";
            return View(await _context.AppUsers.Where(e => entryPoint.Contains(e.Id)).ToListAsync());
        }

        /*
        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.tbAppointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Rating,Report")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.tbAppointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Price,Rating,Report")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
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
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.tbAppointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var appointment = await _context.tbAppointments.FindAsync(id);
            _context.tbAppointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(long id)
        {
            return _context.tbAppointments.Any(e => e.Id == id);
        }*/
    }
}
