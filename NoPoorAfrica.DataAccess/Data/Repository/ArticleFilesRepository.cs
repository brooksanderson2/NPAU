using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class ArticleFilesRepository : Repository<ArticleFiles>, IArticleFilesRepository
    {

        private readonly ApplicationDbContext _db;

        public ArticleFilesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetArticleFilesList()
        {
            return _db.ArticleFiles.Select(i => new SelectListItem()
            {
                Text = i.FilePath,
                Value = i.Id.ToString()
            });
        }

        public void Update(ArticleFiles articleFiles)
        {
            var objFromDb = _db.ArticleFiles.FirstOrDefault(s => s.Id == articleFiles.Id);

            objFromDb.FilePath = articleFiles.FilePath;
            objFromDb.ArticleId = articleFiles.ArticleId;

            _db.SaveChanges();
        }
    }

}
