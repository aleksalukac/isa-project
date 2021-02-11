using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;

namespace Pharmacy.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetById(long id)
        {
            return await _context.tbOrders.FindAsync(id);
        }

        public async Task<List<Order>> GetByPharmacyAndId(long pharmacyId, long id)
        {
            var orders = await _context.tbOrders.Include(x => x.DrugAndQuantities).
                Where(x => x.DrugAndQuantities.PharmacyId == pharmacyId && x.Id == id).ToListAsync();

            return orders;
        }

        public async Task<bool> IsOrderCompleted(long id)
        {
            bool isCompleted = await (from order in _context.tbOrders
                                     where order.Id == id 
                                     select order.TransactionComplete).FirstOrDefaultAsync();

            return isCompleted;
        }

        public async Task<int> Update(Order order)
        {
            _context.Update(order);

            return await _context.SaveChangesAsync();
        }

        public bool Exists(long id)
        {
            return _context.tbOrders.Any(e => e.Id == id);
        }
    }
}
