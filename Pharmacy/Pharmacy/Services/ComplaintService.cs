using Pharmacy.Data;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class ComplaintService
    {
        private readonly ApplicationDbContext _context;

        public ComplaintService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Update(Complaint complaint)
        {
            _context.Update(complaint);

            return await _context.SaveChangesAsync();
        }

        public bool Exists(long id)
        {
            return _context.Complaint.Any(e => e.Id == id);
        }
    }
}
