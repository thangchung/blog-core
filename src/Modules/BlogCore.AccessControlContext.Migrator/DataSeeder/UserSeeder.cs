using BlogCore.AccessControlContext.Domain;
using BlogCore.AccessControlContext.Infrastructure;
using BlogCore.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogCore.AccessControlContext.Migrator.DataSeeder
{
    public static class UserSeeder
    {
        public static async Task Seed(IdentityServerDbContext dbContext)
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);
            await roleStore.CreateAsync(new IdentityRole
            {
                Id = IdHelper.GenerateId().ToString(),
                Name = "user",
                NormalizedName = "user"
            });
            await roleStore.CreateAsync(new IdentityRole
            {
                Id = IdHelper.GenerateId().ToString(),
                Name = "admin",
                NormalizedName = "admin"
            });

            var userStore = new UserStore<AppUser>(dbContext);
            var password = new PasswordHasher<AppUser>();
            var rootUser = new AppUser
            {
                Id = IdHelper.GenerateId("fc7d420b-4613-43af-9160-549cd3540222").ToString(),
                UserName = "root",
                Email = "root@blogcore.com",
                NormalizedEmail = "root@blogcore.com",
                NormalizedUserName = "root@blogcore.com",
                SecurityStamp = IdHelper.GenerateId().ToString("D"),
                LockoutEnabled = true,
                FamilyName = "Mr",
                GivenName = "Root",
                ProfilePhotoPath = "https://static.productionready.io/images/smiley-cyrus.jpg",
                Location = "Vietnam",
                Bio = "I am a super user",
                Company = "@NashTech"
            };

            var normalUser = new AppUser
            {
                Id = IdHelper.GenerateId("4b5f26ce-df97-494c-b747-121d215847d8").ToString(),
                UserName = "thangchung",
                Email = "thangchung@blogcore.com",
                NormalizedEmail = "thangchung@blogcore.com",
                NormalizedUserName = "thangchung@blogcore.com",
                SecurityStamp = IdHelper.GenerateId().ToString("D"),
                LockoutEnabled = true,
                BlogId = IdHelper.GenerateId("34c96712-2cdf-4e79-9e2f-768cb68dd552"),
                FamilyName = "Chung",
                GivenName = "Thang",
                ProfilePhotoPath = "https://static.productionready.io/images/smiley-cyrus.jpg",
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