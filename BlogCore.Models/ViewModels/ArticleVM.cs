using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.Models.ViewModels{
    public class ArticleVM
    {
        //posible error
        public ArticleModel vm_Article { get; set; }
        public IEnumerable<SelectListItem> vm_ListCategories { get; set; }
    }
}