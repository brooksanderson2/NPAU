using System;
using System.Collections.Generic;
using System.Text;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.DataAccess.Data.Repository.IRepository
{
    public interface IStoreItemRepository : IRepository<StoreItem>
    {
        void Update(StoreItem StoreItem);
    }
}
