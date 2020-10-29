using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.DataAccess.Repository.IRepository
{
    public interface IPurchaseHistoryRepository : IRepository<PurchaseHistory>
    {
        
        void Update(PurchaseHistory purchaseHistory);

        public int IncrementCount(PurchaseHistory purchaseHistory, int count);
        public int DecrementCount(PurchaseHistory purchaseHistory, int count);

    }
}
