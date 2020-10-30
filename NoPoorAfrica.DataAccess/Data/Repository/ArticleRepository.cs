using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;


namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {

        private readonly ApplicationDbContext _db;

        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetArticleList()
        {
            return _db.Article.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.Id.ToString()
            });
        }

        public void Update(Article article)
        {
            var objFromDb = _db.Article.FirstOrDefault(s => s.Id == article.Id);

            objFromDb.Title = article.Title;
            objFromDb.TitleFont = article.TitleFont;
            objFromDb.Body = article.Body;
            objFromDb.BodyFont = article.BodyFont;
            objFromDb.BodyTextSize = article.BodyTextSize;
            objFromDb.RouteName = article.RouteName;
            objFromDb.Template = article.Template;

            _db.SaveChanges();
        }
    }
}
