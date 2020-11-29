using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using NoPoorAfrica.Utility;

namespace NoPoorAfrica.Pages.User.Store
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ShoppingCart ShoppingCartObj { get; set; }

        public void OnGet(int id)
        {
            ShoppingCartObj = new ShoppingCart()
            {
                StoreItem = _unitOfWork.StoreItem.GetFirstOrDefault(includeProperties: "Category,Size",
                filter: c => c.Id == id),
                StoreItemId = id
            };
        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                    //Determind the GUID of the logged in user
                    var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                    ShoppingCartObj.ApplicationUserId = claim.Value;

                    ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId && c.StoreItemId == ShoppingCartObj.StoreItemId);
                    if (cartFromDb == null)
                    {
                        _unitOfWork.ShoppingCart.Add(ShoppingCartObj);
                    }
                    else //update
                    {
                        _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, ShoppingCartObj.Count);
                    }
                    _unitOfWork.Save();

                    var count = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).ToList().Count;
                    //Establish the session for count
                    HttpContext.Session.SetInt32(SD.ShoppingCart, count);
                    return RedirectToPage("Index");
            }

            else
            {
                ShoppingCartObj.StoreItem = _unitOfWork.StoreItem.GetFirstOrDefault(includeProperties: "Category,Size",
                filter: c => c.Id == ShoppingCartObj.StoreItem.Id);
                return Page();
            }
        }
    }
}
