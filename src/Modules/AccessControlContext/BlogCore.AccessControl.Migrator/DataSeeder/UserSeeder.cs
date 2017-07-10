using System;
using System.Threading.Tasks;
using BlogCore.AccessControl.Domain;
using BlogCore.AccessControl.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogCore.AccessControl.Migrator.DataSeeder
{
    public static class UserSeeder
    {
        public static async Task Seed(IdentityServerDbContext dbContext)
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);
            await roleStore.CreateAsync(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "user",
                NormalizedName = "user"
            });
            await roleStore.CreateAsync(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NormalizedName = "admin"
            });

            var userStore = new UserStore<AppUser>(dbContext);
            var password = new PasswordHasher<AppUser>();
            var rootUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "root",
                Email = "root@blogcore.com",
                NormalizedEmail = "root@blogcore.com",
                NormalizedUserName = "root@blogcore.com",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = true,
                FamilyName = "Mr",
                GivenName = "Root",
                Location = "Vietnam",
                Bio = "I am a super user",
                Company = "@NashTech"
            };

            var normalUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "thangchung",
                Email = "thangchung@blogcore.com",
                NormalizedEmail = "thangchung@blogcore.com",
                NormalizedUserName = "thangchung@blogcore.com",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = true,
                BlogId = new Guid("34c96712-2cdf-4e79-9e2f-768cb68dd552"),
                FamilyName = "Chung",
                GivenName = "Thang",
                Location = "Saigon - Vietnam",
                Bio = "A software developer and architect",
                Company = "@NashTech"
            };

            rootUser.PasswordHash = password.HashPassword(rootUser, "r00t");
            normalUser.PasswordHash = password.HashPassword(normalUser, "thangchung");

            // add users
            await userStore.CreateAsync(rootUser);
            await userStore.CreateAsync(normalUser);

            // assign roles
            await userStore.AddToRoleAsync(rootUser, "admin");
            await userStore.AddToRoleAsync(normalUser, "user");
        }
    }
}