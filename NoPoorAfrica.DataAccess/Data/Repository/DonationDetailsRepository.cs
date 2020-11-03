using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class DonationDetailsRepository : Repository<DonationDetails>, IDonationDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public DonationDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
