using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;

namespace Pharmacy.Services
{
    public class DrugAndQuantitiesService : IDrugAndQuantitiesService
    {
        private readonly ApplicationDbContext _context;

        public DrugAndQuantitiesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public DrugAndQuantitiesService()
        {
        }

        public async Task<DrugAndQuantities> GetById(long id)
        {
            return await _context.DrugAndQuantity.FindAsync(id);
        }

        public async Task<int> Update(DrugAndQuantities drugAndQuantities)
        {
            _context.Update(drugAndQuantities);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> Remove(DrugAndQuantities drugAndQuantities)
        {
            if(drugAndQuantities == null || !Exists(drugAndQuantities.Id))
            {
                return false;
            }

            _context.Remove(drugAndQuantities);

            await _context.SaveChangesAsync();

            return true;
        }

        public bool Exists(long id)
        {
            return _context.DrugAndQuantity.Any(e => e.Id == id);
        }
    }
}
