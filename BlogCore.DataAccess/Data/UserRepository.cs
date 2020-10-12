using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.DataAccess.Data{
    public class UserRepository : Repository<ApplicationUserModel>, IUserRepository
    {
        private readonly ApplicationDbContext db;

        public UserRepository(ApplicationDbContext db): base(db)
        {
            this.db = db;
        }

        public void LockUser(string userID)
        {
            var user = db.ApplicationUser.FirstOrDefault(u => u.Id == userID);
            user.LockoutEnd = DateTime.Now.AddDays(14);
            db.SaveChanges();
        }

        public void UnlockUser(string userID)
        {
            var user = db.ApplicationUser.FirstOrDefault(u => u.Id == userID);
            user.LockoutEnd = DateTime.Now;
            db.SaveChanges();
        }
    }
}