﻿//using FirstDemo.Domain.Entities;
using StackOverFlow.Infrastructure;
//using FirstDemo.Infrastructure.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StackOverFlow.Infrastructure
{
    public class ApplicationDbContext :IdentityDbContext,
        IApplicationDbContext
    {
		private readonly string _connectionString;
		private readonly string _migrationAssembly;

		public ApplicationDbContext(string connectionString, string migrationAssembly)
		{
			_connectionString = connectionString;
			_migrationAssembly = migrationAssembly;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionString,
					x => x.MigrationsAssembly(_migrationAssembly));
			}

			base.OnConfiguring(optionsBuilder);
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
			/*builder.Entity<CourseEnrollment>().ToTable("CourseEnrollments");

            builder.Entity<CourseEnrollment>().HasKey(x => new { x.CourseId, x.StudentId });

			builder.Entity<CourseEnrollment>()
				.HasOne<Course>()
				.WithMany()
				.HasForeignKey(x => x.CourseId);

            builder.Entity<CourseEnrollment>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(x => x.StudentId);


            builder.Entity<Course>().HasData(new Course[]
			{
				new Course{ Id = new Guid("003805c3-938c-43b7-a768-03d6c0242ece"), Title = "Test Course 1", Description = "Test", Fees = 2000 },
				new Course{ Id = new Guid("aff7c36d-6dc1-48b6-9793-323d66ddeb35"), Title = "Test Course 2", Description = "Test", Fees = 3000 }
			});*/

            base.OnModelCreating(builder);
        }

    }
}