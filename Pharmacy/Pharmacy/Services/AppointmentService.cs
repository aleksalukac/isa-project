using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class AppointmentService
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

        public async Task<List<Appointment>> GetByUser(string id)
        {
            var appointments = await(from appointment in _context.tbAppointments
                                       where appointment.MedicalExpertID == id
                                       select appointment).ToListAsync();

            return appointments;
        }

    }
}
