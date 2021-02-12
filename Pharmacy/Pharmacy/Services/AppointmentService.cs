using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAbsenceRequestService _absenceRequestService;
        public AppointmentService(ApplicationDbContext context, IAbsenceRequestService absenceRequestService)
        {
            _context = context;
            _absenceRequestService = absenceRequestService;
        }

        public async Task<Appointment> GetById(long id)
        {
            return await _context.tbAppointments
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Appointment>> GetAll()
        {
            return await _context.tbAppointments.ToListAsync();
        }

        public async Task<List<Appointment>> GetAllForPharmacy(long id)
        {
            var appointments = await (from appointment in _context.tbAppointments
                                      where appointment.PhrmacyId == id
                                      select appointment).ToListAsync();

            return appointments;
        }

        public async Task<List<Appointment>> GetByMedicalExpert(string id)
        {
            var appointments = await (from appointment in _context.tbAppointments
                                      where appointment.MedicalExpertID == id
                                      select appointment).ToListAsync();

            return appointments;
        }

        public async Task<List<Appointment>> GetByPatient(string id)
        {
            var appointments = await (from appointment in _context.tbAppointments
                                      where appointment.PatientID == id
                                      select appointment).ToListAsync();

            return appointments;
        }

        public async Task<List<Appointment>> GetFreeDermatologistApp()
        {
            AppointmentType type = AppointmentType.Exam;
            var appointments = await _context.tbAppointments.Where(x => x.Type == type && x.PatientID == null).ToListAsync();

            return appointments;
        }

        public async Task<List<Appointment>> GetCurrentByMedicalExpert(string id)
        {
            List<Appointment> appointments = await GetByMedicalExpert(id);

            for (int i = appointments.Count - 1; i >= 0; i--)
            {
                if(!(appointments[i].StartDateTime <= DateTime.Now &&
                    (appointments[i].StartDateTime + appointments[i].Duration) >= DateTime.Now))
                {
                    appointments.RemoveAt(i);
                }
            }

            return appointments;
        }

        public async Task<int> Update(Appointment appointment)
        {
            if (await IsPossibleTime(appointment))
            {
                _context.Update(appointment); 
                await _context.SaveChangesAsync();
            }

            return await _context.SaveChangesAsync();
        }

        public bool Exists(long id)
        {
            return _context.tbAppointments.Any(e => e.Id == id);
        }

        public void Remove(Appointment appointment)
        {
            _context.Remove(appointment);
        }

        public bool IsOverlapping(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            if (!((start1 > start2) && (start1 > end2) && (end1 > start1) && (end1 > end2)))
                return false;
            if (!((start1 < start2) && (start1 < end2) && (end1 < start1) && (end1 < end2)))
                return false;

            return true;
        }

        public async Task<bool> IsPossibleTime(Appointment appointment)
        {
            var medExpertAppointments = await GetByMedicalExpert(appointment.MedicalExpertID);
            var patientAppointments = appointment.PatientID == null ? new List<Appointment>() :
                                                                    await GetByPatient(appointment.PatientID);
            var absenceRequests = await _absenceRequestService.GetByUser(appointment.MedicalExpertID);

            foreach(var app in medExpertAppointments)
            {
                if(IsOverlapping(appointment.StartDateTime, appointment.StartDateTime + appointment.Duration, app.StartDateTime, app.StartDateTime + app.Duration))
                {
                    return false;
                }
            }

            // Local - changes that are made and still waiting in the transaction (changes that are
            // not in the database but soon will be)
            // Instead of passive concurrency, we will use locals and boost our optimistic concurrency
            // Passive concurrency is forbidden in Entity framework 
            foreach(var app in _context.tbAppointments.Local)
            {
                if(app.MedicalExpertID == appointment.MedicalExpertID || app.PatientID == appointment.PatientID)
                {
                    if(IsOverlapping(app.StartDateTime, app.StartDateTime + app.Duration, appointment.StartDateTime, appointment.StartDateTime + appointment.Duration))
                    {
                        return false;
                    }
                }
            }

            foreach (var app in patientAppointments)
            {
                if (IsOverlapping(appointment.StartDateTime, appointment.StartDateTime + appointment.Duration, app.StartDateTime, app.StartDateTime + app.Duration))
                {
                    return false;
                }
            }

            foreach (var app in absenceRequests)
            {
                if (IsOverlapping(app.StartDateTime, app.EndDateTime, appointment.StartDateTime, appointment.StartDateTime + appointment.Duration))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> Create(Appointment appointment)
        {
            if(await IsPossibleTime(appointment))
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<Appointment>> GetByMedicalExpertFree(string id)
        {
            var appointments = await GetByMedicalExpert(id);
            appointments = appointments.Where(x => x.PatientID == null).ToList();
            return (appointments);
        }
    }
}
