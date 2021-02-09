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

        public async Task Update(Order order)
        {
            if(order != null)
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
