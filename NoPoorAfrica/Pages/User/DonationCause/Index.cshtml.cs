using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using NoPoorAfrica.Models.ViewModels;

namespace NoPoorAfrica.Pages.Donation
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DonationCauseVM DonationCause { get; set; }
        public IEnumerable<Models.Models.DonationCause> DonationCauseList { get; set; }

        public IEnumerable<Models.Models.Donation> DonationList { get; set; }
        public IEnumerable<Models.Models.DonationDetails> DonationDetailsList { get; set; }


        public void OnGet()
        {
            DonationCauseList = _unitOfWork.DonationCause.GetAll();
            DonationDetailsList = _unitOfWork.DonationDetails.GetAll();


            foreach (var DonationCause in DonationCauseList) 
            {
                DonationCauseList = _unitOfWork.DonationCause.GetAll();
                DonationDetailsList = _unitOfWork.DonationDetails.GetAll(c => c.DonationCauseId == DonationCause.Id);


                foreach (var DonationDetails in DonationDetailsList)
                {
                    DonationCause.GoalProgress += DonationDetails.DonationTotal;
                }
            }
        }
    }
}
