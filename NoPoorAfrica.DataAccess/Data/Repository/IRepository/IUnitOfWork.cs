using NoPoorAfrica.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {


        IShoppingCartRepository ShoppingCart { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IPurchaseHistoryRepository PurchaseHistory { get; }
        IDonationCauseRepository DonationCause { get; }
        IDonationRepository Donation { get; }
        IStoreItemRepository StoreItem { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ICategoryRepository Category { get; }
        ISizeRepository Size { get; }
        IArticleRepository Article { get; }

        void Save();
    }
}
