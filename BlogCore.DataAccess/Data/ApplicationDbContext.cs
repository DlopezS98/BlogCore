﻿using System;
using System.Collections.Generic;
using System.Text;
using BlogCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ArticleModel> Articles { get; set; }
        public DbSet<SliderModel> Sliders { get; set; }
        public DbSet<ApplicationUserModel> ApplicationUser { get; set; }
    }
}
