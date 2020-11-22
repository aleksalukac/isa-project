using ISA.Models.Entities;
using ISA.Models.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISA.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Drug> tbDrugs;

        public DbSet<Report> tbReports { get; set; }

        public DbSet<Pharmacy> tbPharmacys;

        public DbSet<Order> tbOrders;

        public DbSet<Appointment> tbAppointments;

        public DbSet<AbsenceRequest> tbAbsenceRequests;

        public DbSet<SupplyOrder> tbSupplyOrders;

        public DbSet<SupplyOffer> tbSupplyOffers;

        public DbSet<User> tbUsers;

        public DbSet<Dermatologist> tbDermatologist;

        public DbSet<Pharmacist> tbPharmacist;

        public DbSet<Supplier> tbSuppliers;
        
        public DbSet<SystemAdministrator> tbSystemAdministrator;

        public DbSet<PharmacyAdministrator> tbPharmacyAdministrator;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().ToTable("Report");
        }
    }
}
