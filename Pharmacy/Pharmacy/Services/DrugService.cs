using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class DrugService : IDrugService
    {
        private readonly ApplicationDbContext _context;

        public DrugService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Drug>> GetAll()
        {
            return await (from drug in _context.tbDrugs
                            select drug).ToListAsync();
        }

        public async Task<List<Drug>> GetByPatientNoAllergies(string patientId)
        {
            var drugs = await GetAll();

            var userWithAllergies = await _context.AppUsers.Include(x => x.AllergicDrugs).FirstOrDefaultAsync(x => x.Id == patientId);

            var allergies = userWithAllergies.AllergicDrugs;

            for(int i = drugs.Count - 1; i >=0; i--)
            {
                if(allergies.Select(x => x.Id).Contains(drugs[i].Id))
                {
                    drugs.RemoveAt(i);
                }
            }

            return drugs;
        }

        public async Task<List<DrugAndQuantities>> GetAllByPharmacy(long pharmacyId)
        {
            return await (from drugAndQuantities in _context.DrugAndQuantity
                          where drugAndQuantities.PharmacyId == pharmacyId
                          select drugAndQuantities).ToListAsync();
        }
    }
}
