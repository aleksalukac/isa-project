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

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
