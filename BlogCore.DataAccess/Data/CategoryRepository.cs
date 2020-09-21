using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.DataAccess.Data{
    public class CategoryRepository : Repository<CategoryModel>, ICategoryRepository
    {
        private readonly ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext _db): base(_db)
        {
            this.db = _db;
        }

        public IEnumerable<SelectListItem> GetListCategories()
        {
            return db.Categories.Select(i => new SelectListItem(){
                Text = i.CategoryName,
                Value = i.CategoryID.ToString()
            });
        }

        public void Update(CategoryModel Category)
        {
            var categoryObject = db.Categories.FirstOrDefault(cat => cat.CategoryID == Category.CategoryID);
            categoryObject.CategoryName = Category.CategoryName;
            categoryObject.Order = Category.Order;
            db.SaveChanges();
        }
    }
}