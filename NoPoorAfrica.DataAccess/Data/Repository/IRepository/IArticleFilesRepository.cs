using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository.IRepository
{
    public interface IArticleFilesRepository : IRepository<ArticleFiles>
    {
        IEnumerable<string> GetByArticleAscending(int id);

        public int GetLastPosition(int ArticleId);

        public Dictionary<string, int> GetByArticleWithPosition(int ArticleId);

        void Update(ArticleFiles articleFiles);
    }
}
