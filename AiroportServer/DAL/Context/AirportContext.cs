﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class AirportContext:DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> dbContextOptions):base(dbContextOptions) { }



        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Flight> Flights { get; set; }

        public DbSet<Luggage> Luggage { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureBooking(modelBuilder.Entity<Booking>());
            ConfigureEmployee(modelBuilder.Entity<Employee>());
            ConfigureFlight(modelBuilder.Entity<Flight>());

            ConfigureUserToken(modelBuilder.Entity<UserToken>());

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureUserToken(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(userToken => userToken.Token);
        }
        private void ConfigureBooking(EntityTypeBuilder<Booking> builder)
        {
            builder
                .HasMany(booking => booking.Luggage)
                .WithOne(luggage => luggage.Booking)
                .HasForeignKey(luggage => luggage.BookingId)
                .IsRequired();

            builder
                .HasOne(booking =>booking.Flight)
                .WithMany(flight=> flight.Bookings)
                .HasForeignKey(booking => booking.FlightsId)
                .IsRequired();
        
            builder
                .HasOne(booking => booking.User)
                .WithMany()
                .HasForeignKey(booking => booking.UserId)
                .IsRequired();
        }
       
        private void ConfigureEmployee(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasOne(employee => employee.User)
                .WithOne(user => user.Employee)
                .HasForeignKey<Employee>(employee => employee.UserId)
                .IsRequired();
        }
        private void ConfigureFlight(EntityTypeBuilder<Flight> builder)
        {
            builder
                .HasOne(flight => flight.Plane)
                .WithMany()
                .HasForeignKey(flight => flight.PlaneId)
                .IsRequired();

            builder
                .HasMany(flight => flight.Crew)
                .WithMany();

            builder
                .HasOne(flight => flight.Terminal)
                .WithMany()
                .HasForeignKey(flight => flight.TerminalId)
                .IsRequired();

        }



    }
}
