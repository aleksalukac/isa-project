using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class AbsenceRequestService : IAbsenceRequestService
    {
        private readonly ApplicationDbContext _context;

        public AbsenceRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AbsenceRequest>> GetByUser(string id)
        {
            return await (from absenceRequest in _context.tbAbsenceRequests
                            where absenceRequest.EmployeeId == id
                            select absenceRequest).ToListAsync();
        }

        public async Task<int> Update(AbsenceRequest absenceRequest)
        {
            _context.Update(absenceRequest);

            return await _context.SaveChangesAsync();
        }

        public bool Exists(long id)
        {
            return _context.tbAbsenceRequests.Any(e => e.Id == id);
        }

        public async Task<List<AbsenceRequest>> GetAll()
        {
            return await _context.tbAbsenceRequests.ToListAsync();
        }

        public async Task<AbsenceRequest> GetById(long id)
        {
            return await _context.tbAbsenceRequests
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
