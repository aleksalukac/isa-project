using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pharmacy.Data;
using Pharmacy.Models.DTO;
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

        public async Task<List<DrugDTO>> GetAllFiltered(string searchString, string filter,string pharmacyId, string sort)
        {
            List<DrugDTO> drugs;
            if (long.TryParse(pharmacyId, out long pharmacyId2))
            {
                drugs = await (from drug in _context.tbDrugs
                               join drugsQuant in _context.DrugAndQuantity on drug equals drugsQuant.Drug
                               join pharmacy in _context.tbPharmacys on drugsQuant.PharmacyId equals pharmacy.Id
                               where drugsQuant.PharmacyId == pharmacyId2 && drugsQuant.Quantity > 0
                               select new DrugDTO(drug.Id, drugsQuant.Id, drug.Name, drug.Type, drug.Form, drug.Ingredients, drug.Drugmaker, drug.IsPrescribable, drug.AverageScore, pharmacy.Name, drugsQuant.Price)).ToListAsync();
            }
            else
            {
                drugs = await (from drug in _context.tbDrugs
                               join drugsQuant in _context.DrugAndQuantity on drug equals drugsQuant.Drug
                               join pharmacy in _context.tbPharmacys on drugsQuant.PharmacyId equals pharmacy.Id
                               where drugsQuant.Quantity > 0
                               select new DrugDTO(drug.Id, drugsQuant.Id, drug.Name, drug.Type, drug.Form, drug.Ingredients, drug.Drugmaker, drug.IsPrescribable, drug.AverageScore, pharmacy.Name, drugsQuant.Price)).ToListAsync();
            }
            if(sort == "AverageScore")
            {
                drugs = drugs.OrderBy(x => x.AverageScore).ToList();
            }else if(sort == "Name")
            {
                drugs = drugs.OrderBy(x => x.Name).ToList();
            }
            List<DrugDTO> filteredDrugs = new List<DrugDTO>();

            if (string.IsNullOrEmpty(searchString))
            {
                filteredDrugs = drugs;
            }
            else foreach (var drug in drugs)
                {
                    var json = JsonConvert.SerializeObject(drug);
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (filter == "Form")
                    {
                        if (drug.Form.ToString().ToUpper().Contains(searchString.ToUpper()))
                        {
                            filteredDrugs.Add(drug);
                        }
                    }
                    if (dictionary[filter] != null && dictionary[filter].ToUpper().Contains(searchString.ToUpper()))
                    {
                        filteredDrugs.Add(drug);
                    }
                }
            return filteredDrugs;
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
