using Clients.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clients.SqlServer.Setup
{
    /// <summary>
    /// Represents all the client's entity configuration.
    /// To be use by the ORM entity framework.
    /// </summary>
    public static class ClientsSetup
    {
        /// <summary>
        /// Configures the client's ORM handler, following the business rules.
        /// </summary>
        /// <param name="modelBuilder">The ORM entity builder interface.</param>
        public static void ConfigureClients(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Client>()
                .Property(c => c.FullName)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.UserName)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .Property(c => c.UserName)
                .HasMaxLength(35)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(c => c.Email)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(c => c.MarriageStatus)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(c => c.BirthDay)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Addresses);
        }
    }
}
