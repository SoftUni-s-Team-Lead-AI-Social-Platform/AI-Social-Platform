namespace AI_Social_Platform.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    
    using static Common.EntityValidationConstants.User;
    using Enums;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Publications = new HashSet<Publication.Publication>();
            this.Friends = new HashSet<ApplicationUser>();
            this.UserSchools = new HashSet<UserSchool>();
        }


        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;


        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public byte[]? ProfilePicture { get; set; }

        public byte[]? CoverPhoto { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey(nameof(Country))]
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; } = null!;


        [ForeignKey(nameof(State))]
        public int? StateId { get; set; }
        public virtual State State { get; set; } = null!;


        public Gender? Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public RelationshipStatus? Relationship { get; set; }


        public virtual ICollection<ApplicationUser> Friends { get; set; }
        public virtual ICollection<Publication.Publication> Publications { get; set; }
        public virtual ICollection<UserSchool> UserSchools { get; set; }
        

    }
}