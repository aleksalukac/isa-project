using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.Models.Entities;

namespace Pharmacy.Services
{
    public interface IOrderService
    {
        public Task<List<Order>> GetByPharmacyAndId(long pharmacyId, long id);
        public Task Update(Order order);
        public Task<bool> IsOrderCompleted(long id);
        public Task<Order> GetById(long id);
    }
}
