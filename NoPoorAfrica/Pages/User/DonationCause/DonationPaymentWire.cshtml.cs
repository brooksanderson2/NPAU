using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using NoPoorAfrica.Utility;
using Stripe;

namespace NoPoorAfrica.Pages.User.DonationCause
{
    public class DonationPaymentWireModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.Extensions.Options.IOptions<AuthSenderOptions> _authOptions;
        public DonationPaymentWireModel(IUnitOfWork unitOfWork, Microsoft.Extensions.Options.IOptions<AuthSenderOptions> authOptions)
        {
            _unitOfWork = unitOfWork;
            _authOptions = authOptions;
        }

        [BindProperty]
        public Models.Models.DonationCause DonationCauseObj { get; set; }

        [BindProperty]
        public DonationDetails DonationDetails { get; set; }

        [BindProperty]
        public PendingWire PendingWire { get; set; }
        public void OnGet(int id)
        {
            DonationCauseObj = _unitOfWork.DonationCause.GetFirstOrDefault(c => c.Id == id);
            PendingWire = new PendingWire();
            if (User.Identity.IsAuthenticated)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if (claim != null)
                {
                    //Retrieve details of the person logged in
                    ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(c => c.Id == claim.Value);
                    PendingWire.DonorName = applicationUser.FullName;
                    PendingWire.Email = applicationUser.Email;
                }
            }
        }

        public IActionResult OnPost()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                PendingWire.UserId = claim.Value;
            }
            else
            {
                PendingWire.UserId = null;
            }
            PendingWire.PaymentStatus = SD.PaymentStatusPending;
            PendingWire.DonationDate = DateTime.Now;
            PendingWire.DonationCauseId = DonationCauseObj.Id;

            //send confirmation email
            EmailSender emailSender;
            emailSender = new EmailSender(_authOptions);
            double amount = PendingWire.DonationTotal;
            emailSender.SendEmailAsync(PendingWire.Email, "Thank you for your donation to No Poor Africa!", "Donation amount: " + amount.ToString("C2") + "\n" + "Donation ID: " + PendingWire.TransactionId);
            
            _unitOfWork.PendingWire.Add(PendingWire);
            _unitOfWork.Save();
            return RedirectToPage("/User/DonationCause/WireInstructions", new { id = PendingWire.Id });
        }
    }
}
