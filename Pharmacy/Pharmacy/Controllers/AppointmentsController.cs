﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.DTO;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Services;

namespace Pharmacy.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AppointmentsController(ApplicationDbContext context, UserManager<AppUser> userManager, 
            IAppointmentService appointmentService, IUserService userService)
        {
            _context = context;
            _appointmentService = appointmentService;
            _userManager = userManager;
            _userService = userService;
        }

        [Authorize(Roles = "PharmacyAdmin")]
        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var appointmentList = _appointmentService.GetAllForPharmacy(user.PharmacyId).Result;

            var appointmentDTOList = new List<AppointmentDTO>();

            foreach(var appointment in appointmentList)
            {
                AppUser medicalExpert = _context.tbAppUsers.Find(appointment.MedicalExpertID);
                AppUser patient = _context.tbAppUsers.Find(appointment.PatientID);

                string patientFullName = patient == null ? "" : patient.FirstName + " " + patient.LastName;
                string medicalExpertFullname = medicalExpert == null ? "" : medicalExpert.FirstName + " " + medicalExpert.LastName;

                appointmentDTOList.Add(new AppointmentDTO(appointment,
                    medicalExpertFullname, patientFullName));
            }

            return View(appointmentDTOList);
        }

        // GET: Calendar
        [Authorize(Roles = "Pharmacist,Dermatologist")]
        public async Task<IActionResult> Calendar()
        {
            var user = await _userManager.GetUserAsync(User);
            var appointmentList = _appointmentService.GetByMedicalExpert(user.Id).Result;

            string codeForFront = "[";

            int i = 0;
            foreach (var appointment in appointmentList)
            {
                if(i++ > 0)
                {
                    codeForFront += ",";
                }
                AppUser medicalExpert = _context.tbAppUsers.Find(appointment.MedicalExpertID);
                AppUser patient = _context.tbAppUsers.Find(appointment.PatientID);

                string patientFullName = patient == null ? "" : patient.FirstName + " " + patient.LastName;

                DateTime endTime = appointment.StartDateTime + appointment.Duration;

                codeForFront += "{ title: '" + patientFullName + "', url: 'Details/" + appointment.Id + "', start: '" + 
                    appointment.StartDateTime.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss") + "', " +
                    "end: '" + endTime.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss") + "'}\n";
            }
            codeForFront += "]";

            codeForFront = codeForFront.Replace("‘", "").Replace("’","");
            ViewData["codeForFront"] = codeForFront;

            return View();
        }

        [Authorize(Roles = "Pharmacist,Dermatologist")]
        // GET: PatientAppointments/id
        public async Task<IActionResult> PatientAppointments(string id = "")
        {
            if(id.Length == 0)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var appointmentList = _appointmentService.GetByPatient(id).Result;

            var appointmentDTOList = new List<AppointmentDTO>();

            foreach (var appointment in appointmentList)
            {
                AppUser medicalExpert = _context.tbAppUsers.Find(appointment.MedicalExpertID);
                AppUser patient = _context.tbAppUsers.Find(appointment.PatientID);

                string patientFullName = patient == null ? "" : patient.FirstName + " " + patient.LastName;
                string medicalExpertFullname = medicalExpert == null ? "" : medicalExpert.FirstName + " " + medicalExpert.LastName;

                appointmentDTOList.Add(new AppointmentDTO(appointment,
                    medicalExpertFullname, patientFullName));
            }

            return View(appointmentDTOList);
        }

        [Authorize(Roles = "Pharmacist,Dermatologist,Patient")]
        // GET: MyAppointments
        public async Task<IActionResult> MyAppointments()
        {
            var user = await _userManager.GetUserAsync(User);
            var appointmentList = await _appointmentService.GetByMedicalExpert(user.Id);

            var appointmentDTOList = new List<AppointmentDTO>();

            foreach (var appointment in appointmentList)
            {
                AppUser medicalExpert = _context.tbAppUsers.Find(appointment.MedicalExpertID);
                AppUser patient = _context.tbAppUsers.Find(appointment.PatientID);

                string patientFullName = patient == null ? "" : patient.FirstName + " " + patient.LastName;
                string medicalExpertFullname = medicalExpert == null ? "" : medicalExpert.FirstName + " " + medicalExpert.LastName;

                appointmentDTOList.Add(new AppointmentDTO(appointment,
                    medicalExpertFullname, patientFullName));
            }

            return View(appointmentDTOList);
        }

        [Authorize(Roles = "Pharmacist,Dermatologist")]
        // GET: CurrentAppointments
        public async Task<IActionResult> Start(long? id)
        {
            if(!id.HasValue)
            {
                return View("MyAppointments");
            }

            Appointment appointment = await _appointmentService.GetById(id.Value);
            var medicalExpert = await _userManager.GetUserAsync(User);
            var patient = await _userService.GetById(appointment.PatientID);

            if (appointment.MedicalExpertID != medicalExpert.Id || appointment.StartDateTime > DateTime.Now ||
                (appointment.StartDateTime + appointment.Duration) < DateTime.Now)
            {
                return View("MyAppointments");
            }

            AppointmentDTO appointmentDTO = new AppointmentDTO(appointment, medicalExpert, patient);

            return View(appointmentDTO);
        }

        [Authorize(Roles = "Pharmacist,Dermatologist")]
        // GET: CurrentAppointments
        public async Task<IActionResult> CurrentAppointments()
        {
            var user = await _userManager.GetUserAsync(User);
            var appointmentList = await _appointmentService.GetCurrentByMedicalExpert(user.Id);

            var appointmentDTOList = new List<AppointmentDTO>();

            foreach (var appointment in appointmentList)
            {
                AppUser medicalExpert = _context.tbAppUsers.Find(appointment.MedicalExpertID);
                AppUser patient = _context.tbAppUsers.Find(appointment.PatientID);

                string patientFullName = patient == null ? "" : patient.FirstName + " " + patient.LastName;
                string medicalExpertFullname = medicalExpert == null ? "" : medicalExpert.FirstName + " " + medicalExpert.LastName;

                appointmentDTOList.Add(new AppointmentDTO(appointment,
                    medicalExpertFullname, patientFullName));
            }

            return View(appointmentDTOList);
        }

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

        [Authorize(Roles = "Pharmacist,Dermatologist")]
        // GET: Appointments/CreateForMyself
        public IActionResult CreateForMyself()
        {
            List<AppUser> entryPoint = (from user in _context.tbAppUsers
                                        join userrole in _context.UserRoles on user.Id equals userrole.UserId
                                        join role in _context.Roles on userrole.RoleId equals role.Id
                                        where role.Name == "Dermatologist"
                                        select user).ToList();

            ViewData["DermatologistList"] = entryPoint;
            return View();
        }

        // GET: Appointments/Create
        [Authorize(Roles = "PharmacyAdmin")]
        public IActionResult Create()
        {
            List<AppUser> entryPoint = (from user in _context.tbAppUsers
                                             join userrole in _context.UserRoles on user.Id equals userrole.UserId
                                             join role in _context.Roles on userrole.RoleId equals role.Id
                                             where role.Name == "Dermatologist"
                                             select user).ToList();

            ViewData["DermatologistList"] = entryPoint;
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PharmacyAdmin")]
        public async Task<IActionResult> Create([Bind("Id,MedicalExpertID,PatientID,Price,Report,StartDateTime,Duration")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var b = await AppoitmenOverlapsAsync(appointment);
                if (!b)
                {
                    _context.Add(appointment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Overlapping));
                }
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

            List<AppUser> entryPoint = await (from user in _context.tbAppUsers
                                             join userrole in _context.UserRoles on user.Id equals userrole.UserId
                                             join role in _context.Roles on userrole.RoleId equals role.Id
                                             where role.Name == "Dermatologist"
                                             select user).ToListAsync();

            ViewData["DermatologistList"] = entryPoint;
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,MedicalExpertID,PatientID,Price,Report,StartDateTime,Duration")] Appointment appointment)
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
        }

        private async Task<bool> AppoitmenOverlapsAsync(Appointment appointment)
        {
            // 1] [2   2] [1
            var appotiments = await _context.tbAppointments.ToListAsync();
            foreach(Appointment appointmentInList in  appotiments)
            {
                if(!((appointmentInList.StartDateTime >= appointment.StartDateTime.Add(appointment.Duration) || (appointmentInList.StartDateTime.Add(appointmentInList.Duration) <= appointment.StartDateTime))
                    && (appointmentInList.StartDateTime >= appointment.StartDateTime && (appointmentInList.StartDateTime.Add(appointmentInList.Duration) <= appointment.StartDateTime.Add(appointmentInList.Duration)))))
                {
                    return true;
                }
            }
            return false;

        }

        public async Task<IActionResult> Overlapping()
        {
            return View();
        }
    }
}
