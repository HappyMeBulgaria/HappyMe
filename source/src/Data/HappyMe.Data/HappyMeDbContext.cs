namespace HappyMe.Data
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using HappyMe.Data.Contracts.Models;
    using HappyMe.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class HappyMeDbContext : IdentityDbContext<User>
    {
        public HappyMeDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }

        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        // TODO: Make many to many table in the context for Modules in Questions
        public virtual DbSet<Module> Modules { get; set; }

        public virtual DbSet<UserInModule> UsersInModules { get; set; }

        public virtual DbSet<UserAnswer> UsersAnswers { get; set; }

        public virtual DbSet<Feedback> Feedback { get; set; }

        public virtual DbSet<ModuleSession> ModuleSessions { get; set; }

        ////public override DbSet<IdentityRole> Roles { get; set; }

        ////public static HappyMeDbContext Create()
        ////{
        ////    return new HappyMeDbContext();
        ////}

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();

            return this.SaveChangesWithTracingDbExceptions();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this.ApplyAuditInfoRules();

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInModule>()
                .HasKey(a => new { a.UserId, a.ModuleId });

            modelBuilder.Entity<Module>()
                .HasMany(m => m.Questions);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Modules);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Author)
                .WithMany(a => a.Questions)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAuditInfoRules()
        {
            var changedAudits = this.ChangeTracker.Entries()
                    .Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in changedAudits)
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private int SaveChangesWithTracingDbExceptions()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Exception currentException = ex;
                while (currentException != null)
                {
                    Trace.TraceError(currentException.Message);
                    currentException = currentException.InnerException;
                }

                throw;
            }

            // catch (DbEntityValidationException ex)
            // {
            //    foreach (var validationErrors in ex.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceError($"Property: {validationError.PropertyName}{Environment.NewLine} Error: {validationError.ErrorMessage}");
            //        }
            //    }

            // throw;
            // }
        }
    }
}
