using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockData.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure
{
    public class ApplicationDbContext: DbContext,IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext(string connectionString,string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StockPrice>().ToTable("StockPrice");

            builder.Entity<Company>().ToTable("Company");

            builder.Entity<StockPrice>()
               .HasOne(x => x.Company)
               .WithMany()
               .HasForeignKey(y => y.CompanyId);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);

        }


        public DbSet<StockPrice> StockPrices { get; set; }

        public DbSet<Company> Companies { get; set; }

    }
}
