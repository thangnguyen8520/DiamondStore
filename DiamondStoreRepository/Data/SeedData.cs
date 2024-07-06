using DiamondBusinessObject.Enums;
using DiamondBusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Data
{
    public class SeedData
    {
        public async Task Initialize(IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            await InitializeRole(roleManager);
            await InitializeAccount(roleManager, userManager);
        }

        private static async Task InitializeRole(RoleManager<IdentityRole> roleManager)
        {
            var roles = Enum.GetNames(typeof(RoleEnums));
            foreach (var roleName in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task InitializeAccount(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            var adminRole = await roleManager.FindByNameAsync(RoleEnums.Admin.ToString());
            if (adminRole != null)
            {
                var adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                    GenderEnum = GenderEnums.Male,
                    StatusEnum = UserStatusEnums.Active
                };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole.Name);
                }
            }

            var staffRole = await roleManager.FindByNameAsync(RoleEnums.Staff.ToString());
            if (staffRole != null)
            {
                var staffUser = new User
                {
                    UserName = "staff",
                    Email = "staff@example.com",
                    EmailConfirmed = true,
                    GenderEnum = GenderEnums.Male,
                    StatusEnum = UserStatusEnums.Active
                };
                var result = await userManager.CreateAsync(staffUser, "Staff@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(staffUser, staffRole.Name);
                }
            }

            var customerRole = await roleManager.FindByNameAsync(RoleEnums.Customer.ToString());
            if (customerRole != null)
            {
                var customerUser = new User
                {
                    UserName = "customer",
                    Email = "customer@example.com",
                    EmailConfirmed = true,
                    GenderEnum = GenderEnums.Male,
                    StatusEnum = UserStatusEnums.Active
                };
                var result = await userManager.CreateAsync(customerUser, "Customer@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customerUser, customerRole.Name);
                }
            }
        }

    }
}
