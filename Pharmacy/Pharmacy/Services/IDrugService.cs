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
    }
}