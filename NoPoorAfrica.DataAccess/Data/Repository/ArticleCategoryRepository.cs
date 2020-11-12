using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class ArticleCategoryRepository : Repository<ArticleCategory>, IArticleCategoryRepository
    {

        private readonly ApplicationDbContext _db;

        public ArticleCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetArticleCategoryList()
        {
            return _db.ArticleCategory.Select(i => new SelectListItem()
            {
                Text = i.Category,
                Value = i.Id.ToString()
            });
        }

        public void Update(ArticleCategory articleCategory)
        {
            var objFromDb = _db.ArticleCategory.FirstOrDefault(s => s.Id == articleCategory.Id);

            objFromDb.Category = articleCategory.Category;
            objFromDb.Description = articleCategory.Description;

            _db.SaveChanges();
        }
    }

}
