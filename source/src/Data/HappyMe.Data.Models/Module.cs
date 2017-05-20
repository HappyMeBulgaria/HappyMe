namespace HappyMe.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Constants;
    using HappyMe.Data.Contracts;
    using HappyMe.Data.Contracts.Models;

    public class Module : DeletableEntity, IIdentifiable<int>
    {
        private ICollection<QuestionInModule> questionsInModules;
        private ICollection<UserInModule> usersInModule;
        private ICollection<ModuleSession> sessions;

        public Module()
        {
            this.questionsInModules = new HashSet<QuestionInModule>();
            this.usersInModule = new HashSet<UserInModule>();
            this.sessions = new List<ModuleSession>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.ModuleNameMinLength)]
        [MaxLength(GlobalConstants.ModuleNameMaxLength)]
        public string Name { get; set; }

        [MinLength(GlobalConstants.ModuleDescriptionMinLength)]
        [MaxLength(GlobalConstants.ModuleDescriptionMaxLength)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public bool IsPublic { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<ModuleSession> Sessions
        {
            get => this.sessions;
            set => this.sessions = value;
        }

        public virtual ICollection<QuestionInModule> QuestionsInModules
        {
            get => this.questionsInModules;
            set => this.questionsInModules = value;
        }

        public virtual ICollection<UserInModule> UsersInModule
        {
            get => this.usersInModule;
            set => this.usersInModule = value;
        }
    }
}
