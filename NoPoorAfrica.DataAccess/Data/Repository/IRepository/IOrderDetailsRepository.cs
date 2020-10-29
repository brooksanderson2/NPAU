using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.DataAccess.Data.Repository.IRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    { 

        void Update(OrderDetails OrderDetails);
    }
}
