using DAL.Models;
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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Luggage> Luggage { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureGate(modelBuilder.Entity<Gate>());

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureGate(EntityTypeBuilder<Gate> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasOne(gate => gate.Terminal)
                .WithMany(terminal => terminal.Gates)
                .HasForeignKey(gate => gate.TerminalId)
                .IsRequired();
        }


    }
}
