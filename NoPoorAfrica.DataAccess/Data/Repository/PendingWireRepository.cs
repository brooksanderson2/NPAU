using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class PendingWireRepository : Repository<PendingWire>, IPendingWireRepository
    {
        private readonly ApplicationDbContext _db;

        public PendingWireRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
