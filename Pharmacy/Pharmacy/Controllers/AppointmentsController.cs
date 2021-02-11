using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private readonly IDrugService _drugService;
        private readonly IPharmacyService _pharmacyService;
        private readonly EmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;

        public AppointmentsController(ApplicationDbContext context, UserManager<AppUser> userManager, 
            IAppointmentService appointmentService, IUserService userService, IDrugService drugService,
            IEmailSender emailSender, IPharmacyService pharmacyService)
        {
            _context = context;
            _appointmentService = appointmentService;
            _userManager = userManager;
            _userService = userService;
            _drugService = drugService;
            _pharmacyService = pharmacyService;
            using (StreamReader r = new StreamReader("./Areas/Identity/emailCredentials.json"))
            {
                string json = r.ReadToEnd();
                _emailSender = JsonConvert.DeserializeObject<EmailSender>(json);
            }
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
                AppUser medicalExpert = await _userService.GetById(appointment.MedicalExpertID);
                AppUser patient = await _userService.GetById(appointment.PatientID);

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
                AppUser medicalExpert = await _userService.GetById(appointment.MedicalExpertID);
                AppUser patient = await _userService.GetById(appointment.PatientID);

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
                AppUser medicalExpert = await _userService.GetById(appointment.MedicalExpertID);
                AppUser patient = await _userService.GetById(appointment.PatientID);

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
                AppUser medicalExpert = await _userService.GetById(appointment.MedicalExpertID);
                AppUser patient = await _userService.GetById(appointment.PatientID);

                string patientFullName = patient == null ? "" : patient.FirstName + " " + patient.LastName;
                string medicalExpertFullname = medicalExpert == null ? "" : medicalExpert.FirstName + " " + medicalExpert.LastName;

                appointmentDTOList.Add(new AppointmentDTO(appointment,
                    medicalExpertFullname, patientFullName));
            }

            return View(appointmentDTOList);
        }

        [Authorize(Roles = "Pharmacist,Dermatologist")]
        // GET: CurrentAppointments
        public async Task<IActionResult> NotShowUp(long? id)
        {
            if (!id.HasValue)
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

            appointment.Duration = DateTime.Now - appointment.StartDateTime;
            appointment.Report = "Didn't show up";

            //Update appointment
            try
            {
                await _appointmentService.Update(appointment);
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

            patient.Penalty++;

            //Update patient - add penalty
            try
            {
                await _userService.Update(patient);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userService.Exists(patient.Id))
                {
                    return NotFound();
                }
                else
                {
                    return View("ConcurrencyError", "Home");
                }
            }

            return View("MyAppointments;");
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

            var drugs = await _drugService.GetByPatientNoAllergies(patient.Id);

            AppointmentExamDTO appointmentDTO = new AppointmentExamDTO(appointment, 
                medicalExpert, patient);

            ViewData["DrugList"] = drugs;

            return View(appointmentDTO);
        }

        [HttpGet("Appointments/ScheduleAppointment/{patientId}")]
        [Authorize(Roles = "Pharmacist,Dermatologist")]
        public async Task<IActionResult> ScheduleAppointment(string patientId = "")
        {
            var user = await _userManager.GetUserAsync(User);

            AppointmentDTO appointmentDTO = new AppointmentDTO();
            if (patientId == "")
                appointmentDTO.PatientID = "NoPatient";
            else
                appointmentDTO.PatientID = patientId;
            appointmentDTO.MedicalExpertID = user.Id;

            ViewData["PatientId"] = patientId;
            ViewData["CurrentDate"] = DateTime.Now.ToString("yyyy-MM-dd");

            List<Appointment> allAppointments = await _appointmentService.GetByMedicalExpert(user.Id);
            if(patientId != "")
                allAppointments.AddRange(await _appointmentService.GetByPatient(patientId));

            List<AppointmentTimeDTO> allAppointmentsTime = new List<AppointmentTimeDTO>();

            foreach(var appointment in allAppointments)
            {
                allAppointmentsTime.Add(new AppointmentTimeDTO(appointment.StartDateTime, appointment.Duration));
            }

            ViewData["appointmentsTime"] = JsonConvert.SerializeObject(allAppointmentsTime);
            ViewData["startWorkingHours"] = user.WorkHoursStart.ToString() == "00:00:00" ? "08:00:00" : user.WorkHoursStart.ToString();
            ViewData["endWorkingHours"] = user.WorkHoursEnd.ToString() == "00:00:00" ? "16:00:00" : user.WorkHoursEnd.ToString();

            appointmentDTO.StartDateTime = DateTime.Now;
            return View("ScheduleAppointment", appointmentDTO);
        }

        [HttpPost("Appointments/ScheduleAppointment/{id}")]
        [Authorize(Roles = "Pharmacist,Dermatologist")]
        public async Task<IActionResult> ScheduleAppointment(string patientId, [Bind("StartDateTime,Duration")]
                                                        AppointmentExamDTO appointmentExamDTO)
        {
            var user = await _userManager.GetUserAsync(User);

            appointmentExamDTO.PatientID = patientId;
            appointmentExamDTO.MedicalExpertID = user.Id;

            return View();
        }

        [Authorize(Roles = "Pharmacist,Dermatologist")]
        public async Task<IActionResult> EndAppointment(long appointmentId, [Bind("AppointmentId,Report,PrescribedDrug,PrescriptionLength")] 
                                                        AppointmentExamDTO appointmentExamDTO)
        {
            Appointment appointment = await _appointmentService.GetById(appointmentId);

            appointment.Report = appointmentExamDTO.Report;

            if (_drugService.GetDrugQuantity(appointmentExamDTO.PrescribedDrug, appointment.PhrmacyId) > 0)
            {
                _drugService.CheckoutDrug(appointmentExamDTO.PrescribedDrug, appointment.PhrmacyId);
                List<Drug> prescribedDrugs = new List<Drug>();
                prescribedDrugs.Add(await _drugService.GetById(appointmentExamDTO.PrescribedDrug));
                appointment.PrescribedDrugs = prescribedDrugs;
                ViewData["DrugCheckout"] = "Drug successfuly given to the patient";
            }
            else
            {
                var drug = await _drugService.GetById(appointmentExamDTO.PrescribedDrug);
                List<Drug> allergies = await _drugService.GetByPatientNoAllergies(appointment.PatientID);
                List<Drug> similarDrugs = await _drugService.GetSimilarDrugs(drug.Id);

                var pharmacyAdmin = await _userService.GetById(await _pharmacyService.GetAdmin(appointment.PhrmacyId));

                await _emailSender.SendEmailAsync(pharmacyAdmin.Email, "Drug not available",
                    $"Drug {drug.Id} not available in the pharmacy {appointment.PhrmacyId} ");

                bool givenDrug = false;

                for(int i = 0; i < similarDrugs.Count; i++)
                {
                    if (_drugService.GetDrugQuantity(similarDrugs[i].Id, appointment.PhrmacyId) > 0 &&
                        allergies.Select(x => x.Id).Contains(similarDrugs[i].Id))
                    {
                        _drugService.CheckoutDrug(appointmentExamDTO.PrescribedDrug, appointment.PhrmacyId);
                        ViewData["DrugCheckout"] = "The drug you prescribed was not available (Pharmacy has been informed), but a suitable alternative " +
                            similarDrugs[i].Name + " was given to the patient. Patient is not allergic to this drug";

                        List<Drug> prescribedDrugs = new List<Drug>();
                        prescribedDrugs.Add(similarDrugs[i]);
                        appointment.PrescribedDrugs = prescribedDrugs;

                        givenDrug = true;
                        break;
                   }
                }
                if(!givenDrug)
                {
                    ViewData["DrugCheckout"] = "Unfortunately, the drug you prescribed and its alternatives were not available.";
                }
            }
            appointment.PrescriptionDuration = appointmentExamDTO.PrescriptionLength;
            appointment.Duration = DateTime.Now - appointment.StartDateTime;

            try
            {
                await _appointmentService.Update(appointment);
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

            @ViewData["PatientId"] = appointment.PatientID;
            return View();
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
                AppUser medicalExpert = await _userService.GetById(appointment.MedicalExpertID);
                AppUser patient = await _userService.GetById(appointment.PatientID);

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

            var appointment = await _appointmentService.GetById(id.Value);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "PharmacyAdmin")]
        public async Task<IActionResult> Create()
        {
            ViewData["DermatologistList"] = await _userService.GetAllByRole("Dermatologist");
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
                var isOverlapping = await AppoitmenOverlapsAsync(appointment);

                if (!isOverlapping)
                {
                    _userService.Create(appointment);
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
        [Authorize(Roles = "Pharmacist,Dermatologist,PharmacyAdmin")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentService.GetById(id.Value);

            if (appointment == null)
            {
                return NotFound();
            }

            ViewData["DermatologistList"] = await _userService.GetAllByRole("Dermatologist");
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pharmacist,Dermatologist,PharmacyAdmin")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,MedicalExpertID,PatientID,Price,Report,StartDateTime,Duration,RowVersion")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _appointmentService.Update(appointment);
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

            var appointment = await _appointmentService.GetById(id.Value);

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
            var appointment = await _appointmentService.GetById(id);

            try
            {
                _appointmentService.Remove(appointment);
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

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AppoitmenOverlapsAsync(Appointment appointment)
        {
            // 1] [2   2] [1
            var appointments = await _appointmentService.GetAll();
            foreach(Appointment appointmentInList in appointments)
            {
                if(!((appointmentInList.StartDateTime >= appointment.StartDateTime.Add(appointment.Duration) || (appointmentInList.StartDateTime.Add(appointmentInList.Duration) <= appointment.StartDateTime))
                    && (appointmentInList.StartDateTime >= appointment.StartDateTime && (appointmentInList.StartDateTime.Add(appointmentInList.Duration) <= appointment.StartDateTime.Add(appointmentInList.Duration)))))
                {
                    return true;
                }
            }
            return false;

        }

        public IActionResult Overlapping()
        {
            return View();
        }
    }
}
