using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class DonationCauseCategoryRepository : Repository<DonationCauseCategory>, IDonationCauseCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public DonationCauseCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetDonationCauseCategoryListForDropDown()
        {
            return _db.DonationCauseCategory.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(DonationCauseCategory donationCauseCategory)
        {
            var objFromDb = _db.DonationCauseCategory.FirstOrDefault(s => s.Id == donationCauseCategory.Id);

            objFromDb.Name = donationCauseCategory.Name;

            _db.SaveChanges();
        }
    }
}
