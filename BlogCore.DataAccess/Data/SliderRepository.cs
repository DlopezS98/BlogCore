using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.DataAccess.Data{
    public class SliderRepository : Repository<SliderModel>, ISliderRepository{
        private readonly ApplicationDbContext db;

        public SliderRepository(ApplicationDbContext _db): base(_db)
        {
            this.db = _db;
        }

        public void Update(SliderModel slider){
            var sliderObject = db.Sliders.FirstOrDefault(sl => sl.Pk_SliderID == slider.Pk_SliderID);
            sliderObject.SliderName = slider.SliderName;
            sliderObject.SliderImageUrl = slider.SliderImageUrl;
            sliderObject.SliderStatus = slider.SliderStatus;
            db.SaveChanges();
        }
    }
}