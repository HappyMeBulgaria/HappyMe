namespace Te4Fest.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Te4Fest.Data.Contracts.Models;
    using Te4Fest.Data.Models;

    public class Te4FestDbContext : IdentityDbContext<User>
    {
        public Te4FestDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual IDbSet<Answer> Answers { get; set; } 

        public virtual IDbSet<Question> Questions { get; set; } 

        public virtual IDbSet<Image> Images { get; set; } 

        public virtual IDbSet<Module> Modules { get; set; }

        public virtual IDbSet<UserInModule> UsersInModules { get; set; }

        public virtual IDbSet<UserAnswer> UsersAnswers { get; set; }

        public static Te4FestDbContext Create()
        {
            return new Te4FestDbContext();
        }

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceError($"Property: {validationError.PropertyName}{Environment.NewLine} Error: {validationError.ErrorMessage}");
                    }
                }

                throw;
            }
        }
    }
}