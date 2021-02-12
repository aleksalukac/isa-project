using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IPharmacyService
    {
        public Task<Pharmacy.Models.Entities.Pharmacy> GetById(long id);
        public Task<string> GetAdmin(long pharmacyId);
        public Task<int> Update(Pharmacy.Models.Entities.Pharmacy pharmacy);
        public Task<List<Models.Entities.Pharmacy>> GetAllFiltered(string searchString, string filter, string sort);
        public Task<List<Models.Entities.Pharmacy>> GetAvailablePharmacies(DateTime dateTime);
        public Task<List<AppUser>> GetAllPharmacists(long pharmacyId, DateTime dateTime);
        public bool Exists(long id);
        public Task<List<Pharmacy.Models.Entities.Pharmacy>> GetAll();
        public Task<List<long>> GetPharmacyByDermatologist(string id);
    }
}