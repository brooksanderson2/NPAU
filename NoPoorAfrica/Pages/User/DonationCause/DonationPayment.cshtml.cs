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
    public class DonationPaymentModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.Extensions.Options.IOptions<AuthSenderOptions> _authOptions;
        public DonationPaymentModel(IUnitOfWork unitOfWork, Microsoft.Extensions.Options.IOptions<AuthSenderOptions> authOptions)
        {
            _unitOfWork = unitOfWork;
            _authOptions = authOptions;
        }

        [BindProperty]
        public Models.Models.DonationCause DonationCauseObj { get; set; }

        [BindProperty]
        public DonationDetails DonationDetails { get; set; }

        public void OnGet(int id)
        {
            DonationCauseObj = _unitOfWork.DonationCause.GetFirstOrDefault(c => c.Id == id);
            DonationDetails = new DonationDetails();
            if (User.Identity.IsAuthenticated)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if (claim != null)
                {
                    //Retrieve details of the person logged in
                    ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(c => c.Id == claim.Value);
                    DonationDetails.DonorName = applicationUser.FullName;
                    DonationDetails.Email = applicationUser.Email;
                }
            }
        }

        public IActionResult OnPost(string stripeToken)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                DonationDetails.UserId = claim.Value;
            }
            else
            {
                DonationDetails.UserId = null;
            }
            DonationDetails.PaymentStatus = SD.PaymentStatusPending;
            DonationDetails.DonationDate = DateTime.Now;
            DonationDetails.DonationCauseId = DonationCauseObj.Id;
            if (stripeToken != null)
            {
                var options = new ChargeCreateOptions
                {
                    //Amount in cents
                    Amount = Convert.ToInt32(DonationDetails.DonationTotal * 100),
                    Currency = "usd",
                    Description = "Donation ID: " + DonationDetails.Id,
                    Source = stripeToken
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

                DonationDetails.TransactionId = charge.Id;

                if (charge.Status.ToLower() == "succeeded")
                {
                    //send confirmation email
                    EmailSender emailSender;
                    emailSender = new EmailSender(_authOptions);
                    double amount = DonationDetails.DonationTotal;
                    decimal a;
                    a = Convert.ToDecimal(amount);
                    emailSender.SendEmailAsync(DonationDetails.Email, "Thank you for your donation to No Poor Africa!", "Donation amount: " + amount.ToString("C2") + "\n" + "Donation ID: " + DonationDetails.TransactionId);

                    DonationDetails.PaymentStatus = SD.PaymentStatusApproved;
                }

                else
                {
                    DonationDetails.PaymentStatus = SD.PaymentStatusRejected;
                }
            }
            _unitOfWork.DonationDetails.Add(DonationDetails);
            _unitOfWork.Save();
            return RedirectToPage("/User/DonationCause/DonationConfirmation", new { id = DonationDetails.TransactionId });
        }
    }
}
