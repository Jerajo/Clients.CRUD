using Clients.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clients.SqlServer.Setup
{
    /// <summary>
    /// Represents all the addresses's entity configuration.
    /// To be use by the ORM entity framework.
    /// </summary>
    public static class AdressesSetup
    {
        /// <summary>
        /// Configures the client's ORM handler, following the business rules.
        /// </summary>
        /// <param name="modelBuilder">The ORM entity builder interface.</param>
        public static void ConfigureAdresses(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Address>()
                .Property(a => a.Country)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.State)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.PostalCode)
                .HasColumnType("decimal(5,0)")
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.AddressLineOne)
                .HasMaxLength(250)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.AddressLineTwo)
                .HasMaxLength(250)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Client);
        }
    }
}
