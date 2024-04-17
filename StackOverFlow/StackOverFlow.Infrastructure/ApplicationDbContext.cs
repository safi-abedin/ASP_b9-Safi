//using FirstDemo.Domain.Entities;
using StackOverFlow.Infrastructure;
//using FirstDemo.Infrastructure.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackOverFlow.Infrastructure.Membership;
using StackOverFlow.Domain.Entities;
using System.Reflection.Emit;
using StackOverFlow.Infrastructure.Data;
using StackOverFlow.Domain;

namespace StackOverFlow.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>,
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
			builder.Entity<Question>()
				.HasMany(x => x.Tags).WithMany(x => x.Questions)
				.UsingEntity(t => t.ToTable("QuestionsTags"));


            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            builder.Entity<QuestionVotes>()
              .HasOne(a => a.Question)
              .WithMany(q => q.Votes)
              .HasForeignKey(a => a.QuestionId);

            builder.Entity<Reply>()
                .HasOne(c => c.Answer)
                .WithMany(a => a.Replies)
                .HasForeignKey(c => c.AnswerId);


            builder.Entity<AnswerVotes>()
                .HasOne(c => c.Answer)
                .WithMany(a => a.AnswerVotes)
                .HasForeignKey(c => c.AnswerId);

            builder.Entity<Tag>().HasData(new TagsSeed().Tags);


            base.OnModelCreating(builder);
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Reply> Replies { get; set; }


        public DbSet<AnswerVotes> AnswerVotes { get; set; }

        public DbSet<QuestionVotes> QuestionVotes { get; set; }

    }
}