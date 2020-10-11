using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Models.ViewModels
{
    public class HomePageVM
    {
        public IEnumerable<SliderModel> VmSliders { get; set; }
        public IEnumerable<ArticleModel> VmArticles { get; set; }
    }
}