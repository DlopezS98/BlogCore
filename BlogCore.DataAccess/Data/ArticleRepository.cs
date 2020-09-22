using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogCore.DataAccess.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.DataAccess.Data
{
    public class ArticleRepository : Repository<ArticleModel>, IArticleRepository
    {
        private readonly ApplicationDbContext db;

        public ArticleRepository(ApplicationDbContext _db) : base(_db)
        {
            this.db = _db;
        }

        public void Update(ArticleModel article)
        {
            var articleObject = db.Articles.FirstOrDefault(cat => cat.ArticleID == article.ArticleID);
            articleObject.ArticleName = article.ArticleName;
            articleObject.ArticleImageUrl = article.ArticleImageUrl;
            articleObject.ArticleDescription = article.ArticleDescription;
            articleObject.ArticleCreationDate = article.ArticleCreationDate;
            articleObject.Fk_CategoryID = article.Fk_CategoryID;
            // db.SaveChanges();
        }
    }
}