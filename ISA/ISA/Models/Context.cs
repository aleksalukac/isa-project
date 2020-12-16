using ISA.Models.Entities;
using ISA.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISA.Models
{
    public class Context : IdentityDbContext<AppUser>//, IdentityRole<long>/*, long*/>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Drug> tbDrugs { get; set; }

        public DbSet<Report> tbReports { get; set; }

        public DbSet<Pharmacy> tbPharmacys { get; set; }

        public DbSet<Order> tbOrders { get; set; }

        public DbSet<Appointment> tbAppointments { get; set; }

        public DbSet<AbsenceRequest> tbAbsenceRequests { get; set; }

        public DbSet<SupplyOrder> tbSupplyOrders { get; set; }

        public DbSet<SupplyOffer> tbSupplyOffers { get; set; }

        public DbSet<User> tbUsers { get; set; }

        public DbSet<Supplier> tbSuppliers { get; set; }

        public DbSet<Employee> tbEmployees { get; set; }

        public DbSet<AppUser> tbAppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Report>().ToTable("Reports");
            modelBuilder.Entity<Drug>().ToTable("Drugs");
            modelBuilder.Entity<Pharmacy>().ToTable("Pharmacys");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<SupplyOrder>().ToTable("SupplyOrders")
                .HasMany(p => p.Order)
                .WithOne();
            modelBuilder.Entity<SupplyOffer>().ToTable("SupplyOffers");
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");
    
            modelBuilder.Entity<AbsenceRequest>().ToTable("AbsenceRequests")
                .HasOne(y => y.Employee)
                .WithMany(x => x.AbsenceRequests)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<AppUser>().ToTable("AppUsers");

        }

        public DbSet<ISA.Models.Entities.DrugType> DrugType { get; set; }

        public DbSet<ISA.Models.Entities.DrugAndQuantity> DrugAndQuantity { get; set; }
    }
}
