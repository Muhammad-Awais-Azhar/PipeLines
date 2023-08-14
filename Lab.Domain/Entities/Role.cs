using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVX.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace SVX.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        [DisplayName("Role Name")]
        [Required]
        [Column(TypeName = "varchar(50)")]
        [MaxLength(50)]
        public string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
