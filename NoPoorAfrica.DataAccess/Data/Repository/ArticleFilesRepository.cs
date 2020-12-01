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

        public IEnumerable<string> GetByArticleAscending(int id)
        {
            return _db.ArticleFiles.OrderBy(i => i.Position).Where(i => i.ArticleId == id).Select(i => i.FilePath);
        }

        public int GetLastPosition(int ArticleId)
        {
            return _db.ArticleFiles.OrderBy(i => i.Position).Where(i => i.ArticleId == ArticleId).Last().Position;
        }

        public int GetLowestAvailablePosition(int ArticleId)
        {
            try
            {
                var Collection = _db.ArticleFiles.OrderBy(i => i.Position).Where(i => i.ArticleId == ArticleId);
                int i = 0;
                foreach (var item in Collection)
                {
                    if (item.Position > i)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                return i;
            }
            catch
            {
                return 0;
            }
        }

        public Dictionary<string,int> GetByArticleWithPosition(int ArticleId)
        {
            var values = _db.ArticleFiles.Where(i => i.ArticleId == ArticleId).ToList();

            Dictionary<string, int> Pairs = new Dictionary<string, int>();
            foreach (var row in values)
            {
                Pairs.Add(row.FilePath, row.Position);
            }

            return Pairs;
        }

        public void Update(ArticleFiles articleFiles)
        {
            var objFromDb = _db.ArticleFiles.FirstOrDefault(s => s.Id == articleFiles.Id);

            objFromDb.FilePath = articleFiles.FilePath;
            objFromDb.Position = articleFiles.Position;
            objFromDb.ArticleId = articleFiles.ArticleId;
            objFromDb.OriginalName = articleFiles.OriginalName;

            _db.SaveChanges();
        }
    }

}
