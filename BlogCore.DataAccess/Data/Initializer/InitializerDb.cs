using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogCore.Models;
using BlogCore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.DataAccess.Data.Initializer
{
    public class InitializerDb : IInitializerDb
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUserModel> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public InitializerDb(ApplicationDbContext db, UserManager<ApplicationUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (db.Database.GetPendingMigrations().Count() > 0)
                {
                    db.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (db.Roles.Any(r => r.Name == Constants.Admin)) return;
            roleManager.CreateAsync(new IdentityRole(Constants.Admin)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(Constants.User)).GetAwaiter().GetResult();
            userManager.CreateAsync(new ApplicationUserModel{
                UserName = "01dlopezs98@gmail.com",
                Email = "01dlopezs98@gmail.com",
                EmailConfirmed = true,
                FirstName = "Danny A. López S."
            }, "${DlopezS98}").GetAwaiter().GetResult();
            
            ApplicationUserModel user = db.ApplicationUser
            .Where(u => u.Email == "01dlopezs98@gmail.com")
            .FirstOrDefault();

            userManager.AddToRoleAsync(user, Constants.Admin).GetAwaiter().GetResult();
        }
    }
}