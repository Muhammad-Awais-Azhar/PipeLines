using Microsoft.EntityFrameworkCore;
using SVX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SVX.Infrastructure.Seed
{
    public static class RoleSeeder
    {
        public static void SeedRoles(AppDbContext dbContext)
        {
            var roles = new List<Role>
    {
        new Role { RoleName = "Administrator" },
        new Role { RoleName = "Owner" },
        new Role { RoleName = "Guest" },
        new Role { RoleName = "Issuer" },
        new Role { RoleName = "Community Member" },
        new Role { RoleName = "Ecosystem Partner" },
        new Role { RoleName = "Anchor Partner" }
    };

            foreach (var role in roles)
            {
                if (!dbContext.Roles.Any(r => r.RoleName == role.RoleName))
                {
                    dbContext.Roles.Add(role);
                }
            }

            dbContext.SaveChanges();

            var adminRole = dbContext.Roles.FirstOrDefault(r => r.RoleName == "Administrator");
            if (adminRole != null)
            {
                var adminUser = new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = "admin@codeninjaconsulting.com",
                    NormalizedUserName = "ADMIN@CODENINJACONSULTING.COM",
                    Email = "admin@codeninjaconsulting.com",
                    NormalizedEmail = "ADMIN@CODENINJACONSULTING.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEBtveRbD+vwmmzihdG6w+27O/dZK9cKp0e/yFsCOrEFkjyYk4z/Oi7votZyqYRQ6vg==",
                    SecurityStamp = "6JQRPL67KEFRJPYVYR44JRLHDKSQ4XUW",
                    ConcurrencyStamp = "2a95b5ff-1f34-4560-95b0-7c04c3a9822f"
                };

                if (!dbContext.Users.Any(u => u.UserName == adminUser.UserName))
                {
                    dbContext.Users.Add(adminUser);
                    dbContext.SaveChanges();

                    var userRole = new UserRole
                    {
                        UserId = adminUser.Id,
                        RoleId = adminRole.Id,
                        CreatedBy="System",
                        CreatedDate = DateTime.Now
                    };

                    if (!dbContext.UserRoles.Any(ur => ur.UserId == userRole.UserId && ur.RoleId == userRole.RoleId))
                    {
                        dbContext.UserRoles.Add(userRole);
                        dbContext.SaveChanges();
                    }
                }
            }
        }


    }
}
