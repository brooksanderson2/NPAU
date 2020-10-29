using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository.IRepository
{
    public interface IDonationRepository : IRepository<Donation>
    {
        void Update(Donation Donation);
    }
}
