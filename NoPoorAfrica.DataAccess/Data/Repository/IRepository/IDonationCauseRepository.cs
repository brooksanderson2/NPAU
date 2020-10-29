using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository.IRepository
{
    public interface IDonationCauseRepository :IRepository<DonationCause>
    {
        void Update(DonationCause DonationCause);
    }
}
