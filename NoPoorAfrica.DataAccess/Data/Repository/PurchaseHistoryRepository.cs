using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.DataAccess.Data;
using NoPoorAfrica.DataAccess.Data.Repository;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.DataAccess.Repository
{
    public class PurchaseHistoryRepository : Repository<PurchaseHistory>, IRepository.IPurchaseHistoryRepository
    {

        private readonly ApplicationDbContext _db;

        public PurchaseHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public IEnumerable<SelectListItem> GetPurchaseHistoryListForDropDown()
        {
            throw new NotImplementedException();
        }

        public void Update(PurchaseHistory purchaseHistory)
        {
            var objFromDb = _db.PurchaseHistory.FirstOrDefault(s => s.ApplicationUserId == purchaseHistory.ApplicationUserId);


            objFromDb.PurchaseDate = purchaseHistory.PurchaseDate;
            objFromDb.ApplicationUserId = purchaseHistory.ApplicationUserId;
            objFromDb.StoreItemId = purchaseHistory.StoreItemId;
        
            _db.SaveChanges();
        }

        public int DecrementCount(PurchaseHistory purchaseHistory, int count)
        {
            purchaseHistory.Count -= count;
            return purchaseHistory.Count;
        }

        public int IncrementCount(PurchaseHistory purchaseHistory, int count)
        {
            purchaseHistory.Count += count;
            return purchaseHistory.Count;
        }
    }
}
