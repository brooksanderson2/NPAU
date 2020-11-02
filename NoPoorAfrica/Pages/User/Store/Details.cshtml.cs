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
                StoreItem = _unitOfWork.StoreItem.GetFirstOrDefault(includeProperties: "Category",
                filter: c => c.Id == id),
                StoreItemId = id
            };
        }


        public IActionResult OnPost()
        {
            
                //Establish the session
                //HttpContext.Session.SetInt32(SD.ShoppingCart, count);
                return RedirectToPage("Index");
          

        }
    }
}
