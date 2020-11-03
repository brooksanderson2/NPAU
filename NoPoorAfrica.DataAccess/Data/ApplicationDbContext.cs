using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

       public DbSet <StoreItem> StoreItem { get; set; }
       public DbSet <Category> Category { get; set; }
       public DbSet <PurchaseHistory> PurchaseHistory { get; set; }
       public DbSet <ShoppingCart> ShoppingCart { get; set; }
       public DbSet <OrderDetails> OrderDetails { get; set; }
       public DbSet <OrderHeader> OrderHeader { get; set; }
       public DbSet <Donation> Donation { get; set; }
       public DbSet <DonationCause> DonationCause { get; set; }
       public DbSet <DonationDetails> DonationDetails { get; set; }
    }
}
