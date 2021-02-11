using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Drug> tbDrugs { get; set; }

        public DbSet<Report> tbReports { get; set; }

        public DbSet<Models.Entities.Pharmacy> tbPharmacys { get; set; }

        public DbSet<Order> tbOrders { get; set; }

        public DbSet<Appointment> tbAppointments { get; set; }

        public DbSet<AbsenceRequest> tbAbsenceRequests { get; set; }

        public DbSet<SupplyOrder> tbSupplyOrders { get; set; }

        public DbSet<SupplyOffer> tbSupplyOffers { get; set; }

        public DbSet<AppUser> tbAppUsers { get; set; }

        public DbSet<DrugType> DrugType { get; set; }

        public DbSet<DrugAndQuantities> DrugAndQuantity { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Complaint> Complaint { get; set; }

        public DbSet<Rating> Rating { get; set; }

        public DbSet<SaleItems> SaleItems { get; set; }

        public DbSet<UserSubscribed> UserSubscribed { get; set; }

        public DbSet<SupplyItem> SupplyItems { get; set; }

        public DbSet<DermatologistPharmacy> DermatologistPharmacy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<AppUser>().ToTable("AppUsers")
                .HasMany(p => p.AbsenceRequests)
                .WithOne();

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");

            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            modelBuilder.Entity<Report>().ToTable("Reports");

            modelBuilder.Entity<Drug>().ToTable("Drugs");

            modelBuilder.Entity<Pharmacy.Models.Entities.Pharmacy>().ToTable("Pharmacys");

            modelBuilder.Entity<Order>().ToTable("Orders");

            modelBuilder.Entity<Appointment>().ToTable("Appointments");

            modelBuilder.Entity<SupplyOrder>().ToTable("SupplyOrders");

            modelBuilder.Entity<SupplyOffer>().ToTable("SupplyOffers");

            modelBuilder.Entity<AbsenceRequest>().ToTable("AbsenceRequests");

            modelBuilder.Entity<UserSubscribed>().ToTable("UserSubscribed");

            modelBuilder.Entity<SupplyItem>().ToTable("SupplyItems");
        }
    }
}