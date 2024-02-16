using Microsoft.AspNetCore.Identity;
using Core.Utils;
using Core.Entities;
namespace Cards.Helpers
{
    public static class SystemUserSeeder
    {
        public static async Task SeedInitialUsers(UserManager<SystemUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var users = InitialUsers();

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, $"123{user.FirstName}");
                }

                await SeedInitialRoles(roleManager);

                foreach (var user in users)
                {
                    if (user.FirstName.Equals("John") || user.FirstName.Equals("Super"))
                    {
                        await userManager.AddToRoleAsync(user, WellKnown.SystemRoles.ADMIN);

                        continue;
                    }

                    await userManager.AddToRoleAsync(user, WellKnown.SystemRoles.MEMBER);
                }

            }
        }

        private static async Task SeedInitialRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole {Name=WellKnown.SystemRoles.ADMIN},
                new IdentityRole {Name=WellKnown.SystemRoles.MEMBER}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        private static List<SystemUser> InitialUsers()
        {
            return new List<SystemUser>
            {
                new SystemUser
            {
                Email = "superadmin@cards.com",
                FirstName = "Super",
                LastName = "Admin",
                UserName = "superadmin@cards.com",
                CreatedDate= DateTime.Now
            },
                new SystemUser
            {
                Email = "johndoe@cards.com",
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe@cards.com",
                CreatedDate= DateTime.Now
            },
                new SystemUser
            {
                Email = "janedoe@cards.com",
                FirstName = "Jane",
                LastName = "Doe",
                UserName = "janedoe@cards.com",
                CreatedDate= DateTime.Now
            },
                new SystemUser
            {
                Email = "member@cards.com",
                FirstName = "Member",
                LastName = "Member",
                UserName = "member@cards.com",
                CreatedDate= DateTime.Now
            }
        };
        }
    }
}
