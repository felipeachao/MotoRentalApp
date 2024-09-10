using MotoRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MotoRental.Infrastructure
{
    public class MotoRentalDbContext : DbContext
    {
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public MotoRentalDbContext(DbContextOptions<MotoRentalDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryPerson>().HasIndex(d => d.Cnpj).IsUnique();
            modelBuilder.Entity<DeliveryPerson>().HasIndex(d => d.CnhNumber).IsUnique();
            modelBuilder.Entity<Moto>().HasIndex(m => m.LicensePlate).IsUnique();
        }
    }
}
