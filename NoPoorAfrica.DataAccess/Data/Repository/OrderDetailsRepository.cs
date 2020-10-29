
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {

        private readonly ApplicationDbContext _db;

        public OrderDetailsRepository(ApplicationDbContext  db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetails OrderDetails)
        {
           
        }
    }
}
