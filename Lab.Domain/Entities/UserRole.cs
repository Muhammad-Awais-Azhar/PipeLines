using SVX.Domain.Base;
using SVX.Domain.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVX.Domain.Entities
{
    
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
