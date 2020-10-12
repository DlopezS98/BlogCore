using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogCore.DataAccess.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MyBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IWorkUnit iworUnit;

        public UsersController(IWorkUnit iworUnit)
        {
            this.iworUnit = iworUnit;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var currentUser = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var users = iworUnit.User.GetAll(u => u.Id != currentUser.Value);
            return View(users);
        }

        public IActionResult LockUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            iworUnit.User.LockUser(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UnlockUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            iworUnit.User.UnlockUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}