using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.DataAccess.Data.Repository
{
    public interface IWorkUnit : IDisposable
    {
        ICategoryRepository Category { get; }

        IArticleRepository Article {get; }
        void Save();
    }
}