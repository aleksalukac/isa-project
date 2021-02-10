using Pharmacy.Data;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
