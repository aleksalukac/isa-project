using Pharmacy.Models.Entities.Users;
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
        public bool Exists(long id);
    }
}