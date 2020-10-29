using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class DonationRepository : Repository<Donation>, IDonationRepository
    {
        private readonly ApplicationDbContext _db;
        public DonationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Donation Donation)
        {
        }
    }
}
