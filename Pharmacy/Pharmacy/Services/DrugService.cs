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

        public async Task<List<Drug>> GetSimilarDrugs(long drugId)
        {
            return await _context.tbDrugs.Include(x => x.SimilarDrugs).Where(x => x.Id == drugId).ToListAsync();
        }

        public async Task<List<Drug>> GetByPatientNoAllergies(string patientId)
        {
            var drugs = await GetAll();

            var userWithAllergies = await _context.AppUsers.Include(x => x.AllergicDrugs).FirstOrDefaultAsync(x => x.Id == patientId);

            var allergies = userWithAllergies.AllergicDrugs;

            for (int i = drugs.Count - 1; i >= 0; i--)
            {
                if (allergies.Select(x => x.Id).Contains(drugs[i].Id))
                {
                    drugs.RemoveAt(i);
                }
            }

            return drugs;
        }

        public int GetDrugQuantity(long drugId, long pharmacyId)
        {
            var quantity = _context.DrugAndQuantity.Include(x => x.Drug)
                .Where(x => x.Drug.Id == drugId && x.PharmacyId == pharmacyId).Select(x => (decimal)x.Quantity).Sum();

            return (int)quantity;
        }

        public async Task<Drug> GetById(long id)
        {
            return await _context.tbDrugs.FindAsync(id);
        }

        public void CheckoutDrug(long drugId, long pharmacyId)
        {
            var drugAndQuantity = _context.DrugAndQuantity.Include(x => x.Drug)
                .Where(x => x.Drug.Id == drugId && x.PharmacyId == pharmacyId).FirstOrDefault();

            drugAndQuantity.Quantity--;

            _context.Update(drugAndQuantity);

            _context.SaveChanges();
        }

        public async Task<List<DrugAndQuantities>> GetAllByPharmacy(long pharmacyId)
        {
            return await (from drugAndQuantities in _context.DrugAndQuantity
                          where drugAndQuantities.PharmacyId == pharmacyId
                          select drugAndQuantities).ToListAsync();
        }

        public async Task<int> Update(Drug drug)
        {
            _context.Update(drug);

            return await _context.SaveChangesAsync();
        }

        public bool Exists(long id)
        {
            return _context.tbDrugs.Any(e => e.Id == id);
        }
    }
}
