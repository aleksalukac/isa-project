using Pharmacy.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IDrugService
    {
        public Task<List<Drug>> GetAll();
        public Task<List<Drug>> GetByPatientNoAllergies(string patientId);
        public Task<List<DrugAndQuantities>> GetAllByPharmacy(long pharmacyId);
        public int GetDrugQuantity(long drugId, long pharmacyId);
        public void CheckoutDrug(long drugId, long pharmacyId);
        public Task<Drug> GetById(long id);
        public Task<List<Drug>> GetSimilarDrugs(long drugId);
        public Task<int> Update(Drug drug);
        public bool Exists(long id);
    }
}