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
        public IStoreItemRepository StoreItem { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public ISizeRepository Size { get; private set; }
        public IDonationDetailsRepository DonationDetails { get; private set; }
        public IArticleRepository Article { get; private set; }
        public IDonationCauseCategoryRepository DonationCauseCategory { get; private set; }
        public IArticleCategoryRepository ArticleCategory { get; private set; }
        public IArticleFilesRepository ArticleFiles { get; private set; }
        public ITEMP_ArticleUploads TempUploads { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            PurchaseHistory = new PurchaseHistoryRepository(_db);
            Donation = new DonationRepository(_db);
            DonationCause = new DonationCauseRepository(_db);
            StoreItem = new StoreItemRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Category = new CategoryRepository(_db);
            Size = new SizeRepository(_db);
            DonationDetails = new DonationDetailsRepository(_db);
            Article = new ArticleRepository(_db);
            DonationCauseCategory = new DonationCauseCategoryRepository(_db);
            ArticleCategory = new ArticleCategoryRepository(_db);
            ArticleFiles = new ArticleFilesRepository(_db);
            TempUploads = new TEMP_ArticleUploads(_db);
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
