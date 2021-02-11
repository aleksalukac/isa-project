using Pharmacy.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IAbsenceRequestService
    {
        public Task<List<AbsenceRequest>> GetByUser(string id);

        public Task<int> Update(AbsenceRequest absenceRequest);

        public bool Exists(long id);
    }
}