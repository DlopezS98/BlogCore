using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using BlogCore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace MyBlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IWorkUnit iworkUnit;
        private readonly IWebHostEnvironment hostEnvironment;

        public ArticlesController(IWorkUnit _iworkUnit, IWebHostEnvironment host)
        {
            this.iworkUnit = _iworkUnit;
            this.hostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticleVM articlevm = new ArticleVM()
            {
                vm_Article = new ArticleModel(),
                vm_ListCategories = iworkUnit.Category.GetListCategories()
            };

            return View(articlevm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM artvm)
        {
            if (ModelState.IsValid)
            {
                //Obteniendo la ruta principal del proyecto (wwwroot)
                string mainPath = hostEnvironment.WebRootPath;
                //Obteniendo la solicitud de la subida de los archivos...
                var files = HttpContext.Request.Form.Files;
                if (artvm.vm_Article.ArticleID == 0)
                {
                    //Nuevo Articulo
                    //Creación de un hash para los nombre de las imagenes de modo que sean unicas
                    string fileName = Guid.NewGuid().ToString();
                    //Estableciendo la ruta donde se guardarán las imagenes
                    var uploads = Path.Combine(mainPath, @"images\articles");
                    var extension = Path.GetExtension(files[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    artvm.vm_Article.ArticleImageUrl = @"\images\articles\" + fileName + extension;
                    artvm.vm_Article.ArticleCreationDate = DateTime.Now.ToString();

                    iworkUnit.Article.Add(artvm.vm_Article);
                    iworkUnit.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            artvm.vm_ListCategories = iworkUnit.Category.GetListCategories();
            return View(artvm);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticleVM artvm = new ArticleVM()
            {
                vm_Article = new ArticleModel(),
                vm_ListCategories = iworkUnit.Category.GetListCategories()
            };

            if (id != null)
            {
                artvm.vm_Article = iworkUnit.Article.Get(id.GetValueOrDefault());
            }

            return View(artvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM artvm)
        {
            if (ModelState.IsValid)
            {
                //Obteniendo la ruta principal del proyecto (wwwroot)
                string mainPath = hostEnvironment.WebRootPath;
                //Obteniendo la solicitud de la subida de los archivos...
                var files = HttpContext.Request.Form.Files;
                var articleObject = iworkUnit.Article.Get(artvm.vm_Article.ArticleID);
                if (files.Count() > 0)
                {
                    //Actualizar Imagen
                    //Creación de un hash para los nombre de las imagenes de modo que sean unicas
                    string fileName = Guid.NewGuid().ToString();
                    //Estableciendo la ruta donde se guardarán las imagenes
                    var uploads = Path.Combine(mainPath, @"images\articles");
                    var newExtension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(mainPath, articleObject.ArticleImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        //Eliminado la imagen anterior
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + newExtension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    artvm.vm_Article.ArticleImageUrl = @"\images\articles\" + fileName + newExtension;
                    artvm.vm_Article.ArticleCreationDate = articleObject.ArticleCreationDate;

                    iworkUnit.Article.Update(artvm.vm_Article);
                    iworkUnit.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //No reemplazar la imagen, conservar la que existe en la bd
                    artvm.vm_Article.ArticleImageUrl = articleObject.ArticleImageUrl;
                    artvm.vm_Article.ArticleCreationDate = articleObject.ArticleCreationDate;
                }

                iworkUnit.Article.Update(artvm.vm_Article);
                iworkUnit.Save();

                return RedirectToAction(nameof(Index));

            }

            return View(artvm);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articleObject = iworkUnit.Article.Get(id);
            string directoryMainPath = hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(directoryMainPath, articleObject.ArticleImageUrl.TrimStart('\\'));
            if(System.IO.File.Exists(imagePath)){
                System.IO.File.Delete(imagePath);
            }

            if(articleObject == null){
                return Json(new {success = false, message = "Error al borrar el artículo"});
            }

            iworkUnit.Article.Remove(articleObject);
            iworkUnit.Save();
            return Json(new {success = true, message = "Artículo borrado con éxito"});
        }

        #region Calls To Api´s
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = iworkUnit.Article.GetAll(includeProperties: "Category") });
        }
        
        #endregion
    }
}