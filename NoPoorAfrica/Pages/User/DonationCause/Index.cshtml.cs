using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;

namespace NoPoorAfrica.Pages.Donation
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Models.Models.DonationCause> DonationCauseList { get; set; }

        public IEnumerable<Models.Models.Donation> DonationList { get; set; }


        public void OnGet()
        {
           DonationCauseList = _unitOfWork.DonationCause.GetAll();
           DonationList = _unitOfWork.Donation.GetAll();        
        }
    }
}
