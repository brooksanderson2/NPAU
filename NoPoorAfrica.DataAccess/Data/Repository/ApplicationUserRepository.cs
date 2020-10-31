
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {

        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext  db) : base(db)
        {
            _db = db;
        }
    }
    
}
