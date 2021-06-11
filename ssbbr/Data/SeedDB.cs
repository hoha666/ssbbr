using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ssbbr.Data.DataModels;

namespace ssbbr.Data
{
    public class SeedDB
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            try
            {
                IdentityRole adminRole = new IdentityRole()
                {
                    Name = "Government"
                };
                await roleManager.CreateAsync(adminRole);
                IdentityUser user = new IdentityUser()
                {
                    Email = "government@ssbbr.ir",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "government@ssbbr.ir",
                    PhoneNumber = "+989337962978",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "Admin123.");
                var createdUser = await userManager.FindByEmailAsync(user.Email);
                await userManager.AddToRoleAsync(createdUser, "Government");

                adminRole = new IdentityRole()
                {
                    Name = "ContentManager"
                };
                await roleManager.CreateAsync(adminRole);
                user = new IdentityUser()
                {
                    Email = "contentmanager@ssbbr.ir",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "contentmanager@ssbbr.ir",
                    PhoneNumber = "+989337962978",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "Admin123.");
                createdUser = await userManager.FindByEmailAsync(user.Email);
                await userManager.AddToRoleAsync(createdUser, "ContentManager");
                //#region DashboardMenus
                //adminRole = await roleManager.FindByNameAsync(adminRole.Name);

                //DashboardMenu Users = new DashboardMenu()
                //{
                //    Depth = 0,
                //    Id = Guid.NewGuid().ToString(),
                //    Title = "مدیریت کاربران",
                //    Url = "#"
                //};
                //context.DashboardMenus.Add(Users);
                //context.SaveChanges();
                //DashboardMenu UsersManagement = new DashboardMenu()
                //{
                //    Depth = 1,
                //    Id = Guid.NewGuid().ToString(),
                //    Title = "لیست کاربران",
                //    Url = "listusers",
                //    ParentId = Users.Id,
                //};
                //context.DashboardMenus.Add(UsersManagement);
                //context.SaveChanges();
                //RoleMenu rm = new RoleMenu()
                //{
                //    MenuId = UsersManagement.Id,
                //    RoleId = adminRole.Id,
                //    DashboardMenus = UsersManagement,
                //    Roles = adminRole
                //};
                //context.RoleMenus.Add(rm);
                //context.SaveChanges();

                //DashboardMenu addUser = new DashboardMenu()
                //{
                //    Depth = 1,
                //    Id = Guid.NewGuid().ToString(),
                //    Title = "افزودن کاربر",
                //    Url = "addUser",
                //    ParentId = Users.Id
                //};

                //context.DashboardMenus.Add(addUser);
                //context.SaveChanges();
                //RoleMenu rm2 = new RoleMenu()
                //{
                //    MenuId = addUser.Id,
                //    RoleId = adminRole.Id,
                //    DashboardMenus = addUser,
                //    Roles = adminRole
                //};
                //context.RoleMenus.Add(rm2);
                //context.SaveChanges();
                //RoleMenu rm3 = new RoleMenu()
                //{
                //    MenuId = Users.Id,
                //    RoleId = adminRole.Id,
                //    DashboardMenus = Users,
                //    Roles = adminRole
                //};
                //context.RoleMenus.Add(rm3);
                //context.SaveChanges();


                //#endregion
            }
            catch (Exception ex) { }
        }
    }
}
