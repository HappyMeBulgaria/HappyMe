namespace HappyMe.Data
{
    //using System;
    //using System.Data.Entity;
    //using System.Data.Entity.Infrastructure;
    //using System.Data.Entity.Validation;
    //using System.Diagnostics;
    //using System.Linq;
    //using System.Threading;
    //using System.Threading.Tasks;

    //using HappyMe.Data.Configurations;
    //using HappyMe.Data.Contracts.Models;
    //using HappyMe.Data.Models;

    //using Microsoft.AspNet.Identity.EntityFramework;

    public class HappyMeDbContext
    {
        //public HappyMeDbContext()
        //    : base("DefaultConnection")
        //{
        //}

        //public virtual IDbSet<Answer> Answers { get; set; }

        //public virtual IDbSet<Question> Questions { get; set; }

        //public virtual IDbSet<Image> Images { get; set; }

        //public virtual IDbSet<Module> Modules { get; set; }

        //public virtual IDbSet<UserInModule> UsersInModules { get; set; }

        //public virtual IDbSet<UserAnswer> UsersAnswers { get; set; }

        //public virtual IDbSet<Feedback> Feedback { get; set; }

        //public virtual IDbSet<ModuleSession> ModuleSessions { get; set; }

        //public override IDbSet<IdentityRole> Roles { get; set; }

        public static HappyMeDbContext Create()
        {
            return new HappyMeDbContext();
        }

        //public override int SaveChanges()
        //{
        //    this.ApplyAuditInfoRules();

        //    return this.SaveChangesWithTracingDbExceptions();
        //}

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        //{
        //    this.ApplyAuditInfoRules();
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new QuestionConfiguration());

        //    base.OnModelCreating(modelBuilder);
        //}

        //private void ApplyAuditInfoRules()
        //{
        //    var changedAudits = this.ChangeTracker.Entries()
        //            .Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

        //    foreach (var entry in changedAudits)
        //    {
        //        var entity = (IAuditInfo)entry.Entity;

        //        if (entry.State == EntityState.Added)
        //        {
        //            if (!entity.PreserveCreatedOn)
        //            {
        //                entity.CreatedOn = DateTime.Now;
        //            }
        //        }
        //        else
        //        {
        //            entity.ModifiedOn = DateTime.Now;
        //        }
        //    }
        //}

        //private int SaveChangesWithTracingDbExceptions()
        //{
        //    try
        //    {
        //        return base.SaveChanges();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        Exception currentException = ex;
        //        while (currentException != null)
        //        {
        //            Trace.TraceError(currentException.Message);
        //            currentException = currentException.InnerException;
        //        }

        //        throw;
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        foreach (var validationErrors in ex.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceError($"Property: {validationError.PropertyName}{Environment.NewLine} Error: {validationError.ErrorMessage}");
        //            }
        //        }

        //        throw;
        //    }
        //}
    }
}