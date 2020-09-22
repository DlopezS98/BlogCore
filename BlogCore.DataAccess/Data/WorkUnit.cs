using System;
using System.Collections.Generic;
using System.Text;
using BlogCore.DataAccess.Data.Repository;

namespace BlogCore.DataAccess.Data
{
    public class WorkUnit : IWorkUnit
    {
        private readonly ApplicationDbContext db;
        public WorkUnit(ApplicationDbContext _db)
        {
            this.db = _db;
            Category = new CategoryRepository(_db);
            Article = new ArticleRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IArticleRepository Article { get; private set; }

        public void Dispose()
        {
            db.Dispose();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}