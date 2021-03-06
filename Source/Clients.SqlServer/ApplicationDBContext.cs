﻿using Clients.Core.Entities;
using Clients.SqlServer.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Clients.SqlServer
{
    /// <summary>
    /// Represents the application database context, 
    /// allowing the communication between the application and the database.
    /// </summary>
    public class ApplicationDBContext : DbContext
    {
        /// <summary>
        /// Default constructor needed for netcore-ef migration tools.
        /// </summary>
        public ApplicationDBContext() { }

        /// <summary>
        /// Default constructor use with DI in the startup file.
        /// </summary>
        /// <param name="options">The options to be use by the DbContext.</param>
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// ORM access to the data base table Clients.
        /// </summary>
        public virtual DbSet<Client> Clients { get; set; }

        /// <summary>
        /// ORM access to the data base table Addresses.
        /// </summary>
        public virtual DbSet<Address> Addresses { get; set; }
        public IConfiguration Configuration { get; }


        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var migrationsAssembly = typeof(ApplicationDBContext).Assembly.GetName().FullName;

            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"),
                    sql => sql.MigrationsAssembly(migrationsAssembly))
                    .UseLazyLoadingProxies();
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureClients();

            modelBuilder.ConfigureAdresses();

            base.OnModelCreating(modelBuilder);
        }
    }
}
