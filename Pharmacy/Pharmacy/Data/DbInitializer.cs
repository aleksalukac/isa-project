using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //Deleting existing data base, before running existing one
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            

            // CHecking if rooms already exist 
            if (context.tbReports.Any())
            {
                return;   // If it's exist, skip adding new one
            }
            else 
            {
                
            }

            

            context.SaveChanges();

            
        }
    }
}
