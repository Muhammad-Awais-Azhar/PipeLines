using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SVX.Domain.Entities
{
    public class User : IdentityUser<int>
    {
            [DisplayName("First Name")]
            [Required]
            [Column(TypeName = "varchar(50)")]
            [MaxLength(50)]
            public string FirstName { get; set; }

            [DisplayName("Last Name")]
            [Required]
            [Column(TypeName = "varchar(50)")]
            [MaxLength(50)]
            public string LastName { get; set; }

            [NotMapped]
            public string Password { get; set; }

            public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
