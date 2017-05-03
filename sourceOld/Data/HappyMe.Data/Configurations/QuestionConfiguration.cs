namespace HappyMe.Data.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using HappyMe.Data.Models;

    public class QuestionConfiguration : EntityTypeConfiguration<Question>
    {
        public QuestionConfiguration()
        {
            this.HasRequired(q => q.Author)
                .WithMany(u => u.Questions)
                .WillCascadeOnDelete(false);
        }
    }
}
