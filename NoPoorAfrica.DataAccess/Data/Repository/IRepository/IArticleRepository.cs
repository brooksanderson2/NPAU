using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.DataAccess.Data.Repository.IRepository
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<SelectListItem> GetArticleList();

        public List<int> GetArticleIds();

        public List<bool> GetPublishStatus();

        void Update(Article article);
    }
}
