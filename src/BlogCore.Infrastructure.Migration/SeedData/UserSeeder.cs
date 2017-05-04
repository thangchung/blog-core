using System;
using System.Threading.Tasks;
using BlogCore.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogCore.Infrastructure.MigrationConsole.SeedData
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

            rootUser.PasswordHash = password.HashPassword(rootUser, "r00t1@3");
            await userStore.CreateAsync(rootUser);
            await userStore.AddToRoleAsync(rootUser, "admin");
        }
    }
}