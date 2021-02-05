﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pharmacy.Data;

namespace Pharmacy.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210205180143_ISADB16")]
    partial class ISADB16
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.AbsenceRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PharmacyAdministratorId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PharmacyAdministratorId");

                    b.ToTable("AbsenceRequests");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Appointment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AppUserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MedicalExpertID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Report")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("AppUserId1");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Drug", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<float>("AvrageScore")
                        .HasColumnType("real");

                    b.Property<long?>("DrugId")
                        .HasColumnType("bigint");

                    b.Property<string>("Drugmaker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Form")
                        .HasColumnType("int");

                    b.Property<string>("Ingredients")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrescribable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("PharmacyId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("TypeId");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.DrugAndQuantity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("DrugId")
                        .HasColumnType("bigint");

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<long?>("SupplyOrderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SupplyOrderId");

                    b.ToTable("DrugAndQuantity");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.DrugType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("DrugType");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Reserved")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Pharmacy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminUserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("AvrageScore")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pharmacys");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Report", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("PharmacyId")
                        .HasColumnType("bigint");

                    b.Property<string>("ReportText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.SupplyOffer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<float>("Offer")
                        .HasColumnType("real");

                    b.Property<string>("SupplierId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("SupplyOrderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("SupplyOrderId");

                    b.ToTable("SupplyOffers");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.SupplyOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("PharmacyId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.ToTable("SupplyOrders");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.TimeSpan", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset>("Duration")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("TimeSpan");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Users.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("AvrageScore")
                        .HasColumnType("real");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Penalty")
                        .HasColumnType("int");

                    b.Property<long?>("PharmacyId")
                        .HasColumnType("bigint");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PharmacyId");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.AbsenceRequest", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", "Employee")
                        .WithMany("AbsenceRequests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", "PharmacyAdministrator")
                        .WithMany()
                        .HasForeignKey("PharmacyAdministratorId");

                    b.Navigation("Employee");

                    b.Navigation("PharmacyAdministrator");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Appointment", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", null)
                        .WithMany("AppointmentsForMedical")
                        .HasForeignKey("AppUserId");

                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", null)
                        .WithMany("AppointmentsForUser")
                        .HasForeignKey("AppUserId1");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Drug", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Drug", null)
                        .WithMany("SimilarDrugs")
                        .HasForeignKey("DrugId");

                    b.HasOne("Pharmacy.Models.Entities.Pharmacy", null)
                        .WithMany("Drugs")
                        .HasForeignKey("PharmacyId");

                    b.HasOne("Pharmacy.Models.Entities.DrugType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.DrugAndQuantity", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId");

                    b.HasOne("Pharmacy.Models.Entities.Order", null)
                        .WithMany("DrugAndQuantities")
                        .HasForeignKey("OrderId");

                    b.HasOne("Pharmacy.Models.Entities.SupplyOrder", null)
                        .WithMany("Order")
                        .HasForeignKey("SupplyOrderId");

                    b.Navigation("Drug");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Report", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("Pharmacy.Models.Entities.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId");

                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Employee");

                    b.Navigation("Pharmacy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.SupplyOffer", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Users.AppUser", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId");

                    b.HasOne("Pharmacy.Models.Entities.SupplyOrder", "SupplyOrder")
                        .WithMany("SupplyOffers")
                        .HasForeignKey("SupplyOrderId");

                    b.Navigation("Supplier");

                    b.Navigation("SupplyOrder");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.SupplyOrder", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Pharmacy", "Pharmacy")
                        .WithMany("SupplyOrders")
                        .HasForeignKey("PharmacyId");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.TimeSpan", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.AbsenceRequest", null)
                        .WithOne("TimeSpan")
                        .HasForeignKey("Pharmacy.Models.Entities.TimeSpan", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pharmacy.Models.Entities.Appointment", null)
                        .WithOne("TimeSpan")
                        .HasForeignKey("Pharmacy.Models.Entities.TimeSpan", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Users.AppUser", b =>
                {
                    b.HasOne("Pharmacy.Models.Entities.Pharmacy", "Pharmacy")
                        .WithMany()
                        .HasForeignKey("PharmacyId");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.AbsenceRequest", b =>
                {
                    b.Navigation("TimeSpan");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Appointment", b =>
                {
                    b.Navigation("TimeSpan");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Drug", b =>
                {
                    b.Navigation("SimilarDrugs");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Order", b =>
                {
                    b.Navigation("DrugAndQuantities");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Pharmacy", b =>
                {
                    b.Navigation("Drugs");

                    b.Navigation("SupplyOrders");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.SupplyOrder", b =>
                {
                    b.Navigation("Order");

                    b.Navigation("SupplyOffers");
                });

            modelBuilder.Entity("Pharmacy.Models.Entities.Users.AppUser", b =>
                {
                    b.Navigation("AbsenceRequests");

                    b.Navigation("AppointmentsForMedical");

                    b.Navigation("AppointmentsForUser");
                });
#pragma warning restore 612, 618
        }
    }
}
