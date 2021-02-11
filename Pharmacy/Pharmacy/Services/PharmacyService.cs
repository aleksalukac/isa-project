using Pharmacy.Data;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly ApplicationDbContext _context;

        public PharmacyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetAdmin(long pharmacyId)
        {
            Pharmacy.Models.Entities.Pharmacy pharmacy = await _context.tbPharmacys.FindAsync(pharmacyId);
            return pharmacy.AdminUserID;
        }

        public async Task<int> Update(Pharmacy.Models.Entities.Pharmacy pharmacy)
        {
            _context.Update(pharmacy);

            return await _context.SaveChangesAsync();
        }

        public bool Exists(long id)
        {
            return _context.tbPharmacys.Any(e => e.Id == id);
        }

        public async Task<Pharmacy.Models.Entities.Pharmacy> GetById(long id)
        {
            return await _context.tbPharmacys
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
