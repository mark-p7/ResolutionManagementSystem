using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ResolutionManagement.Data
{
    public class IdentitySeedData
    {
        public static async Task Initialize(ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {

            context.Database.EnsureCreated();
            string adminRole = "Admin";
            string memberRole = "Member";
            string password4all = "P@$$w0rd";

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(memberRole));
            }

            if (await userManager.FindByNameAsync("a@a.a") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "a@a.a",
                    Email = "a@a.a"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }

            if (await userManager.FindByNameAsync("1@1.1") == null)
            {
                var user = new IdentityUser
                {
                    Id = "37c1ba03-d67c-437e-ac19-2b38b123c55a",
                    UserName = "1@1.1",
                    Email = "1@1.1"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("2@2.2") == null)
            {
                var user = new IdentityUser
                {
                    Id = "221fedc9-3ad4-492e-bfc0-20f198923a24",
                    UserName = "2@2.2",
                    Email = "2@2.2"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("3@3.3") == null)
            {
                var user = new IdentityUser
                {
                    Id = "d34e5684-030b-4bf1-ba0b-51c424468294",
                    UserName = "3@3.3",
                    Email = "3@3.3"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("4@4.4") == null)
            {
                var user = new IdentityUser
                {
                    Id = "c5955b95-5492-4c7b-a3cb-c749c85e3a16",
                    UserName = "4@4.4",
                    Email = "4@4.4"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("5@5.5") == null)
            {
                var user = new IdentityUser
                {
                    Id = "5559d343-5062-4cd1-b0ae-25301e70a10d",
                    UserName = "5@5.5",
                    Email = "5@5.5"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password4all);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }
        }
    }
}