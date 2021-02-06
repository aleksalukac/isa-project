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

/*
 *          var role = new IdentityRole();
            role.Name = "Pharmacist";
            await _roleManager.CreateAsync(role);
            role = new IdentityRole();
            role.Name = "Dermatologist";
            await _roleManager.CreateAsync(role);
            role = new IdentityRole();
            role.Name = "Supplier";
            await _roleManager.CreateAsync(role);
            role = new IdentityRole();
            role.Name = "PharmacyAdmin";
            await _roleManager.CreateAsync(role);
            role = new IdentityRole();
            role.Name = "Admin";
            await _roleManager.CreateAsync(role);
            role = new IdentityRole();
            role.Name = "User";
            await _roleManager.CreateAsync(role);*/