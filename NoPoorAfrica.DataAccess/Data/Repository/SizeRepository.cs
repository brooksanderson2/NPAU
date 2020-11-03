using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;


namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {

        private readonly ApplicationDbContext _db;

        public SizeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetSizeListForDropDown()
        {
            return _db.Size.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Size size)
        {
            var objFromDb = _db.Size.FirstOrDefault(s => s.Id == size.Id);

            objFromDb.Name = size.Name;
            
            _db.SaveChanges();
        }
    }
}
