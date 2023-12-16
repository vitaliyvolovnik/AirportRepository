﻿// <auto-generated />
using System;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(AirportContext))]
    [Migration("20231202155539_updatePlanes")]
    partial class updatePlanes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("DAL.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("FlightsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FlightsId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("DAL.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DAL.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Post")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salury")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DAL.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArrivalAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartureAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GateId")
                        .HasColumnType("int");

                    b.Property<int>("PlaneId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalAddressId");

                    b.HasIndex("DepartureAddressId");

                    b.HasIndex("GateId");

                    b.HasIndex("PlaneId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("DAL.Models.Gate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("TerminalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TerminalId");

                    b.ToTable("Gates");
                });

            modelBuilder.Entity("DAL.Models.Luggage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<double>("Wiight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("Luggage");
                });

            modelBuilder.Entity("DAL.Models.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxCargoWeigth")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("DAL.Models.Terminal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Terminals");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Models.UserToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Token");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("EmployeeFlight", b =>
                {
                    b.Property<int>("CrewId")
                        .HasColumnType("int");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.HasKey("CrewId", "FlightId");

                    b.HasIndex("FlightId");

                    b.ToTable("EmployeeFlight");
                });

            modelBuilder.Entity("DAL.Models.Booking", b =>
                {
                    b.HasOne("DAL.Models.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Flight", "Flight")
                        .WithMany("Bookings")
                        .HasForeignKey("FlightsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("DAL.Models.Customer", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("DAL.Models.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Employee", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("DAL.Models.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Flight", b =>
                {
                    b.HasOne("DAL.Models.Address", "ArrivalAddress")
                        .WithMany()
                        .HasForeignKey("ArrivalAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Address", "DepartureAddress")
                        .WithMany()
                        .HasForeignKey("DepartureAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Gate", "Gate")
                        .WithMany()
                        .HasForeignKey("GateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Plane", "Plane")
                        .WithMany()
                        .HasForeignKey("PlaneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArrivalAddress");

                    b.Navigation("DepartureAddress");

                    b.Navigation("Gate");

                    b.Navigation("Plane");
                });

            modelBuilder.Entity("DAL.Models.Gate", b =>
                {
                    b.HasOne("DAL.Models.Terminal", "Terminal")
                        .WithMany("Gates")
                        .HasForeignKey("TerminalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Terminal");
                });

            modelBuilder.Entity("DAL.Models.Luggage", b =>
                {
                    b.HasOne("DAL.Models.Booking", "Booking")
                        .WithMany("Luggage")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("DAL.Models.UserToken", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmployeeFlight", b =>
                {
                    b.HasOne("DAL.Models.Employee", null)
                        .WithMany()
                        .HasForeignKey("CrewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Flight", null)
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.Booking", b =>
                {
                    b.Navigation("Luggage");
                });

            modelBuilder.Entity("DAL.Models.Customer", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("DAL.Models.Flight", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("DAL.Models.Terminal", b =>
                {
                    b.Navigation("Gates");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
