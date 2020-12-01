using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System.Web;


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

        public List<int> GetArticleIds()
        {
            return _db.Article.Select(i => i.Id).ToList();
        }

        public List<bool> GetPublishStatus()
        {
            return _db.Article.Select(i => i.IsPublished).ToList();
        }

        public void Update(Article article)
        {
            var objFromDb = _db.Article.FirstOrDefault(s => s.Id == article.Id);

            objFromDb.TitleFont = article.TitleFont;
            objFromDb.Body = article.Body;
            objFromDb.BodyFont = article.BodyFont;
            objFromDb.BodyTextSize = article.BodyTextSize;
            objFromDb.PublishDate = article.PublishDate;
            objFromDb.UpdateDate = article.UpdateDate;
            objFromDb.ArticleCategory = article.ArticleCategory;
            objFromDb.IsPublished = article.IsPublished;

            if (objFromDb.Title == article.Title && objFromDb.RouteName != null)
            // Don't change the route name if it's already been set and the title hasn't changed.
            {

            }
            else if (_db.Article.Any(i => i.RouteName == HttpUtility.UrlEncode(article.Title) && i.Id != article.Id) == false) 
            //Check if route already exists based on title, so that two routes cannot be the same.
            {
                objFromDb.RouteName = HttpUtility.UrlEncode(article.Title);
            }
            else
            {
                objFromDb.RouteName = HttpUtility.UrlEncode( (article.PublishDate.ToShortDateString() + "-" + article.Title) );
            }

            objFromDb.Template = article.Template;
            objFromDb.Title = article.Title;

            _db.SaveChanges();
        }
    }
}
