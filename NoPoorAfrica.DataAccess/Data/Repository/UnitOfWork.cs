using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.DataAccess.Repository;
using NoPoorAfrica.DataAccess.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IPurchaseHistoryRepository PurchaseHistory { get; private set; }
        public IDonationRepository Donation { get; private set; }
        public IDonationCauseRepository DonationCause { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            PurchaseHistory = new PurchaseHistoryRepository(_db);
            Donation = new DonationRepository(_db);
            DonationCause = new DonationCauseRepository(_db);

        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
