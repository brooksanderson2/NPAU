using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class TEMP_ArticleUploads : Repository<_TEMP_ArticleUploads>, ITEMP_ArticleUploads
    {

        private readonly ApplicationDbContext _db;

        public TEMP_ArticleUploads(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }

}