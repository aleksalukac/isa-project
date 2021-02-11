using Pharmacy.Data;
using Pharmacy.Models.Entities.Users;
using Pharmacy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            return await _context.tbPharmacys.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Models.Entities.Pharmacy>> GetAllFiltered(string searchString, string filter, string sort)
        {

            var pharmacies = new List<Models.Entities.Pharmacy>();
            if (sort != null)
            {
                if (sort == "Score")
                {
                    pharmacies = await _context.tbPharmacys.OrderBy(x => x.AverageScore).ToListAsync();
                }
                else if (sort == "Name")
                {
                    pharmacies = await _context.tbPharmacys.OrderBy(x => x.Name).ToListAsync();
                }
                else if (sort == "Adress")
                {
                    pharmacies = await _context.tbPharmacys.OrderBy(x => x.Address).ToListAsync();
                }
                else
                {
                    pharmacies = await _context.tbPharmacys.ToListAsync();
                }
            }
            else
            {
                pharmacies = await _context.tbPharmacys.ToListAsync();
            }

            List<Models.Entities.Pharmacy> filteredPharmacies = new List<Models.Entities.Pharmacy>();

            if (string.IsNullOrEmpty(searchString))
            {
                filteredPharmacies = pharmacies;
            }
            else
            {
                foreach (var user in pharmacies)
                {
                    var json = JsonConvert.SerializeObject(user);
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (dictionary[filter] != null && dictionary[filter].ToUpper().Contains(searchString.ToUpper()))
                    {
                        filteredPharmacies.Add(user);
                    }
                }
            }

            return filteredPharmacies;
        }
    }
}