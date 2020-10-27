using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class StoreItemRepository : Repository<StoreItem>, IStoreItemRepository
    {
        private readonly ApplicationDbContext _db;

        public StoreItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(StoreItem storeItem)
        {
            var storeItemFromDb = _db.StoreItem.FirstOrDefault(s => s.Id == storeItem.Id);

            storeItemFromDb.Name = storeItem.Name;
            storeItemFromDb.CategoryId = storeItem.CategoryId;
            storeItemFromDb.Description = storeItem.Description;
            storeItemFromDb.Price = storeItem.Price;

            if (storeItem.Image != null)
            {
                storeItemFromDb.Image = storeItem.Image;
            }

            _db.SaveChanges();
        }

        
    }
}
