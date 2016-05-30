namespace HappyMe.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Data.Contracts.Models;

    public class Feedback : AuditInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
