using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlogCore.Models;

namespace MyBlogCore.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IWorkUnit iworkUnit;

        public HomeController(IWorkUnit _iworkUnit)
        {
            this.iworkUnit = _iworkUnit;
        }

        public IActionResult Index()
        {
            HomePageVM home_vm = new HomePageVM()
            {
                VmSliders = iworkUnit.Slider.GetAll(),
                VmArticles = iworkUnit.Article.GetAll()
            };
            return View(home_vm);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var articleObj = iworkUnit.Article.GetFirstOrDefault(a => a.ArticleID == id);
            return View(articleObj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
