﻿// <auto-generated />
using System;
using ISA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ISA.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ISA.Models.Entities.AbsenceRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PharmacyAdministratorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PharmacyAdministratorId");

                    b.ToTable("AbsenceRequests");
                });

            modelBuilder.Entity("ISA.Models.Entities.Appointment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("MedicalExpertId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Report")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MedicalExpertId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("ISA.Models.Entities.Drug", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

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

            modelBuilder.Entity("ISA.Models.Entities.DrugAndQuantity", b =>
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

            modelBuilder.Entity("ISA.Models.Entities.DrugType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("DrugType");
                });

            modelBuilder.Entity("ISA.Models.Entities.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ISA.Models.Entities.Pharmacy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pharmacys");
                });

            modelBuilder.Entity("ISA.Models.Entities.Report", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("ReportText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("ISA.Models.Entities.SupplyOffer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<float>("Offer")
                        .HasColumnType("real");

                    b.Property<long?>("SupplierId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SupplyOrderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("SupplyOrderId");

                    b.ToTable("SupplyOffers");
                });

            modelBuilder.Entity("ISA.Models.Entities.SupplyOrder", b =>
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

            modelBuilder.Entity("ISA.Models.Entities.TimeSpan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TimeSpan");
                });

            modelBuilder.Entity("ISA.Models.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ISA.Models.Entities.Users.Employee", b =>
                {
                    b.HasBaseType("ISA.Models.Entities.Users.User");

                    b.Property<long?>("AbsenceTimeId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PharmacyId")
                        .HasColumnType("bigint");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.Property<long?>("WorkingScheduleId")
                        .HasColumnType("bigint");

                    b.HasIndex("AbsenceTimeId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("WorkingScheduleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ISA.Models.Entities.Users.Supplier", b =>
                {
                    b.HasBaseType("ISA.Models.Entities.Users.User");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ISA.Models.Entities.AbsenceRequest", b =>
                {
                    b.HasOne("ISA.Models.Entities.Users.Employee", "Employee")
                        .WithMany("AbsenceRequests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ISA.Models.Entities.Users.Employee", "PharmacyAdministrator")
                        .WithMany()
                        .HasForeignKey("PharmacyAdministratorId");

                    b.Navigation("Employee");

                    b.Navigation("PharmacyAdministrator");
                });

            modelBuilder.Entity("ISA.Models.Entities.Appointment", b =>
                {
                    b.HasOne("ISA.Models.Entities.Users.Employee", "MedicalExpert")
                        .WithMany()
                        .HasForeignKey("MedicalExpertId");

                    b.HasOne("ISA.Models.Entities.Users.User", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId");

                    b.Navigation("MedicalExpert");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ISA.Models.Entities.Drug", b =>
                {
                    b.HasOne("ISA.Models.Entities.Drug", null)
                        .WithMany("SimilarDrugs")
                        .HasForeignKey("DrugId");

                    b.HasOne("ISA.Models.Entities.Pharmacy", null)
                        .WithMany("Drugs")
                        .HasForeignKey("PharmacyId");

                    b.HasOne("ISA.Models.Entities.DrugType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ISA.Models.Entities.DrugAndQuantity", b =>
                {
                    b.HasOne("ISA.Models.Entities.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId");

                    b.HasOne("ISA.Models.Entities.Order", null)
                        .WithMany("DrugAndQuantities")
                        .HasForeignKey("OrderId");

                    b.HasOne("ISA.Models.Entities.SupplyOrder", null)
                        .WithMany("Order")
                        .HasForeignKey("SupplyOrderId");

                    b.Navigation("Drug");
                });

            modelBuilder.Entity("ISA.Models.Entities.Order", b =>
                {
                    b.HasOne("ISA.Models.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ISA.Models.Entities.Report", b =>
                {
                    b.HasOne("ISA.Models.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ISA.Models.Entities.SupplyOffer", b =>
                {
                    b.HasOne("ISA.Models.Entities.Users.Supplier", "Supplier")
                        .WithMany("SupplyOffers")
                        .HasForeignKey("SupplierId");

                    b.HasOne("ISA.Models.Entities.SupplyOrder", "SupplyOrder")
                        .WithMany("SupplyOffers")
                        .HasForeignKey("SupplyOrderId");

                    b.Navigation("Supplier");

                    b.Navigation("SupplyOrder");
                });

            modelBuilder.Entity("ISA.Models.Entities.SupplyOrder", b =>
                {
                    b.HasOne("ISA.Models.Entities.Pharmacy", "Pharmacy")
                        .WithMany("SupplyOrders")
                        .HasForeignKey("PharmacyId");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("ISA.Models.Entities.Users.Employee", b =>
                {
                    b.HasOne("ISA.Models.Entities.TimeSpan", "AbsenceTime")
                        .WithMany()
                        .HasForeignKey("AbsenceTimeId");

                    b.HasOne("ISA.Models.Entities.Users.User", null)
                        .WithOne()
                        .HasForeignKey("ISA.Models.Entities.Users.Employee", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("ISA.Models.Entities.Pharmacy", null)
                        .WithMany("Employees")
                        .HasForeignKey("PharmacyId");

                    b.HasOne("ISA.Models.Entities.TimeSpan", "WorkingSchedule")
                        .WithMany()
                        .HasForeignKey("WorkingScheduleId");

                    b.Navigation("AbsenceTime");

                    b.Navigation("WorkingSchedule");
                });

            modelBuilder.Entity("ISA.Models.Entities.Users.Supplier", b =>
                {
                    b.HasOne("ISA.Models.Entities.Users.User", null)
                        .WithOne()
                        .HasForeignKey("ISA.Models.Entities.Users.Supplier", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ISA.Models.Entities.Drug", b =>
                {
                    b.Navigation("SimilarDrugs");
                });

            modelBuilder.Entity("ISA.Models.Entities.Order", b =>
                {
                    b.Navigation("DrugAndQuantities");
                });

            modelBuilder.Entity("ISA.Models.Entities.Pharmacy", b =>
                {
                    b.Navigation("Drugs");

                    b.Navigation("Employees");

                    b.Navigation("SupplyOrders");
                });

            modelBuilder.Entity("ISA.Models.Entities.SupplyOrder", b =>
                {
                    b.Navigation("Order");

                    b.Navigation("SupplyOffers");
                });

            modelBuilder.Entity("ISA.Models.Entities.Users.User", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("ISA.Models.Entities.Users.Employee", b =>
                {
                    b.Navigation("AbsenceRequests");
                });

            modelBuilder.Entity("ISA.Models.Entities.Users.Supplier", b =>
                {
                    b.Navigation("SupplyOffers");
                });
#pragma warning restore 612, 618
        }
    }
}
