using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class DonationCauseRepository : Repository<DonationCause>, IDonationCauseRepository
    {
        private readonly ApplicationDbContext _db;

        public DonationCauseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DonationCause donationCause)
        {
            var donationCauseFromDb = _db.DonationCause.FirstOrDefault(m => m.Id == donationCause.Id);

            donationCauseFromDb.Title = donationCause.Title;
            donationCauseFromDb.Description = donationCause.Description;
            donationCauseFromDb.FromDate = donationCause.FromDate;
            donationCauseFromDb.ToDate = donationCause.ToDate;
            donationCauseFromDb.FundingGoal = donationCause.FundingGoal;
            donationCauseFromDb.Country = donationCause.Country;
            donationCauseFromDb.GoalProgress = donationCause.GoalProgress;
            donationCauseFromDb.IsActive = donationCause.IsActive;
            donationCauseFromDb.IsFeatured = donationCause.IsFeatured;



            if (donationCause.Image != null)
            {
                donationCauseFromDb.Image = donationCause.Image;
            }

            _db.SaveChanges();
        }
    }
}
