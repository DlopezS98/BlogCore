using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MyBlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IWorkUnit iworkUnit;
        private readonly IWebHostEnvironment hostEnvironment;

        public SlidersController(IWorkUnit _iworkUnit, IWebHostEnvironment _hostEnvironment)
        {
            this.iworkUnit = _iworkUnit;
            this.hostEnvironment = _hostEnvironment;
        }

        [HttpGet]
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
        public IActionResult Create(SliderModel slider)
        {
            if (ModelState.IsValid)
            {
                string mainPath = hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                //Nuevo Slider
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(mainPath, @"images\sliders");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                slider.SliderImageUrl = @"\images\sliders\" + fileName + extension;
                iworkUnit.Slider.Add(slider);
                iworkUnit.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var slider = iworkUnit.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SliderModel slider)
        {
            if (ModelState.IsValid)
            {
                string mainPath = hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var sliderObject = iworkUnit.Slider.Get(slider.Pk_SliderID);

                if (files.Count() > 0)
                {
                    //Nuevo Slider
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(mainPath, @"images\sliders");
                    var newExtension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(mainPath, sliderObject.SliderImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + newExtension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    slider.SliderImageUrl = @"\images\sliders\" + fileName + newExtension;
                    iworkUnit.Slider.Update(slider);
                    iworkUnit.Save();
                }
                else
                {
                    slider.SliderImageUrl = sliderObject.SliderImageUrl;
                }

                iworkUnit.Slider.Update(slider);
                iworkUnit.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        #region Calls to Api´s
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            return Json(new { data = iworkUnit.Slider.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sliderObject = iworkUnit.Slider.Get(id);
            if (sliderObject == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
            }

            iworkUnit.Slider.Remove(sliderObject);
            iworkUnit.Save();
            return Json(new { success = true, message = "Slider borrado correctamente" });
        }
        #endregion
    }
}