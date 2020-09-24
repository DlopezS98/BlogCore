using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.DataAccess.Data.Repository{
    public interface ISliderRepository : IRepository<SliderModel>
    {
        void Update(SliderModel slider);
    }
}