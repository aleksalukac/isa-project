using Pharmacy.Models.Entities;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IDrugAndQuantitiesService
    {
        public Task<DrugAndQuantities> GetById(long id);

        public Task<int> Update(DrugAndQuantities drugAndQuantities);

        public Task<bool> Remove(DrugAndQuantities drugAndQuantities);
        public bool Exists(long id);
    }
}