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

        public void OnGet(int id)
        {
            DonationCauseObj = new Models.Models.DonationCause();
            DonationCauseObj = _unitOfWork.DonationCause.GetFirstOrDefault(c => c.Id == id);
        }

        public IActionResult OnPostStripe()
        {
            Models.Models.DonationCause DonationCauseFromDb = _unitOfWork.DonationCause.GetFirstOrDefault(c => c.Id == DonationCauseObj.Id);
            return RedirectToPage("DonationPayment", new { id = DonationCauseFromDb.Id});
            /*
            if (ModelState.IsValid)
            {
                //Determine the GUID of the logged in user
                var claimsIdentity =
                    (ClaimsIdentity)this.User.Identity;
                var claim =
                    claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                DonationCauseObj.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb =
                    _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId && c.MenuItemId == ShoppingCartObj.MenuItemId);
                if (cartFromDb == null)
                {
                    _unitOfWork.ShoppingCart.Add(ShoppingCartObj);
                }
                else //update
                {
                    _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, ShoppingCartObj.Count);
                }

                _unitOfWork.Save();

                var count = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).ToList().Count();
                //Establish the session
                HttpContext.Session.SetInt32(SD.ShoppingCart, count);
                return RedirectToPage("Index");
            }
            else
            {
                ShoppingCartObj.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(includeProperties: "Category,FoodType",
                filter: c => c.Id == ShoppingCartObj.MenuItemId);
            };
            */
        }

        public IActionResult OnPostWire()
        {
            Models.Models.DonationCause DonationCauseFromDb = _unitOfWork.DonationCause.GetFirstOrDefault(c => c.Id == DonationCauseObj.Id);
            return RedirectToPage("DonationPaymentWire", new { id = DonationCauseFromDb.Id });
            /*
            if (ModelState.IsValid)
            {
                //Determine the GUID of the logged in user
                var claimsIdentity =
                    (ClaimsIdentity)this.User.Identity;
                var claim =
                    claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                DonationCauseObj.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb =
                    _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId && c.MenuItemId == ShoppingCartObj.MenuItemId);
                if (cartFromDb == null)
                {
                    _unitOfWork.ShoppingCart.Add(ShoppingCartObj);
                }
                else //update
                {
                    _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, ShoppingCartObj.Count);
                }

                _unitOfWork.Save();

                var count = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).ToList().Count();
                //Establish the session
                HttpContext.Session.SetInt32(SD.ShoppingCart, count);
                return RedirectToPage("Index");
            }
            else
            {
                ShoppingCartObj.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(includeProperties: "Category,FoodType",
                filter: c => c.Id == ShoppingCartObj.MenuItemId);
            };
            */
        }
    }
}
