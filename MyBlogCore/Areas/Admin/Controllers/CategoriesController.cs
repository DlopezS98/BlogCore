using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IWorkUnit iworkUnit;

        public CategoriesController(IWorkUnit _iworkUnit)
        {
            this.iworkUnit = _iworkUnit;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                iworkUnit.Category.Add(category);
                iworkUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CategoryModel category = new CategoryModel();
            category = iworkUnit.Category.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                iworkUnit.Category.Update(category);
                iworkUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var ObjectCategory = iworkUnit.Category.Get(id);
            if (ObjectCategory == null)
            {
                return Json(new { success = false, message = "Error al borrar la categoría" });
            }

            iworkUnit.Category.Remove(ObjectCategory);
            iworkUnit.Save();
            return Json(new { success = true, message = "Categoría eliminada correctamente!" });
        }

        #region Calls To Api´s
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = iworkUnit.Category.GetAll() });
        }

        #endregion
    }
}