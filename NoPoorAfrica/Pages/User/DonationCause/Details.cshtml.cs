using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;

namespace NoPoorAfrica.Pages.User.DonationCause
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.Models.DonationCause DonationCauseObj { get; set; }
        public IEnumerable<Models.Models.DonationDetails> DonationDetailsList { get; set; }

        public void OnGet(int id)
        {
            DonationCauseObj = new Models.Models.DonationCause();
            DonationCauseObj = _unitOfWork.DonationCause.GetFirstOrDefault(c => c.Id == id);

            DonationDetailsList = _unitOfWork.DonationDetails.GetAll(c => c.DonationCauseId == DonationCauseObj.Id);
            foreach (var DonationDetails in DonationDetailsList)
            {
                DonationCauseObj.GoalProgress += DonationDetails.DonationTotal;
            }
        }

        public IActionResult OnPostStripe()
        {
            Models.Models.DonationCause DonationCauseFromDb = _unitOfWork.DonationCause.GetFirstOrDefault(c => c.Id == DonationCauseObj.Id);
            return RedirectToPage("DonationPayment", new { id = DonationCauseFromDb.Id});
        }

        public IActionResult OnPostWire()
        {
            Models.Models.DonationCause DonationCauseFromDb = _unitOfWork.DonationCause.GetFirstOrDefault(c => c.Id == DonationCauseObj.Id);
            return RedirectToPage("DonationPaymentWire", new { id = DonationCauseFromDb.Id });
        }
    }
}
