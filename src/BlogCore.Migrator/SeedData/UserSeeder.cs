using System;
using System.Threading.Tasks;
using BlogCore.Infrastructure.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogCore.Migrator.SeedData
{
    public static class UserSeeder
    {
        public static async Task Seed(BlogCoreDbContext dbContext)
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
                LockoutEnabled = true
            };

            var normalUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "user1",
                Email = "user1@blogcore.com",
                NormalizedEmail = "user1@blogcore.com",
                NormalizedUserName = "user1@blogcore.com",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = true
            };

            rootUser.PasswordHash = password.HashPassword(rootUser, "r00t");
            normalUser.PasswordHash = password.HashPassword(normalUser, "user1");

            // add users
            await userStore.CreateAsync(rootUser);
            await userStore.CreateAsync(normalUser);

            // assign roles
            await userStore.AddToRoleAsync(rootUser, "admin");
            await userStore.AddToRoleAsync(normalUser, "user");
        }
    }
}