using Pharmacy.Models.Entities.Users;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IPharmacyService
    {
        public Task<string> GetAdmin(long pharmacyId);
        public Task<int> Update(Pharmacy.Models.Entities.Pharmacy pharmacy);
        public bool Exists(long id);
    }
}