using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using NoPoorAfrica.Models.ViewModels;
using NoPoorAfrica.Utility;
using Stripe;

namespace NoPoorAfrica.Pages.User.Cart
{
    public class SummaryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public OrderDetailsCartVM OrderDetailsCart { get; set; }

        public void OnGet()
        {
            OrderDetailsCart = new OrderDetailsCartVM()
            {
                OrderHeader = new NoPoorAfrica.Models.Models.OrderHeader(),
                ListCart = new List<ShoppingCart>()
            };

            OrderDetailsCart.OrderHeader.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                IEnumerable<ShoppingCart> cart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);


                if (cart != null)
                {
                    OrderDetailsCart.ListCart = cart.ToList();
                }

                foreach (var cartList in OrderDetailsCart.ListCart)
                {
                    cartList.StoreItem = _unitOfWork.StoreItem.GetFirstOrDefault(m => m.Id == cartList.StoreItemId);
                    OrderDetailsCart.OrderHeader.OrderTotal += (cartList.StoreItem.Price * cartList.Count); //Subtotal
                }

                OrderDetailsCart.OrderHeader.OrderTotal += OrderDetailsCart.OrderHeader.OrderTotal * SD.SalesTaxPercent;

                //Retrieve details of the person logged in 
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(c => c.Id == claim.Value);
                //OrderDetailsCart.OrderHeader.DeliveryName = applicationUser.FullName;
                OrderDetailsCart.OrderHeader.PurchaseDate = DateTime.Now;
                //OrderDetailsCart.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;

            }
        }

        public IActionResult OnPost(string stripeToken, bool? emailbox)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            OrderDetailsCart.ListCart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToList();

            OrderDetailsCart.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            OrderDetailsCart.OrderHeader.PurchaseDate = DateTime.Now;
            OrderDetailsCart.OrderHeader.UserId = claim.Value;
            OrderDetailsCart.OrderHeader.Status = SD.StatusSubmitted;
            bool email = emailbox ?? false;
            if (email) {
                OrderDetailsCart.OrderHeader.EmailPreference = true;
                    }
            else {
                OrderDetailsCart.OrderHeader.EmailPreference = false; 
            }
           

            _unitOfWork.OrderHeader.Add(OrderDetailsCart.OrderHeader);
            _unitOfWork.Save();

            List<OrderDetails> orderDetailsList = new List<OrderDetails>();

            foreach (var item in OrderDetailsCart.ListCart)
            {
                item.StoreItem = _unitOfWork.StoreItem.GetFirstOrDefault(m => m.Id == item.StoreItemId);
                OrderDetails orderDetails = new OrderDetails
                {
                    StoreItemId = item.StoreItemId,
                    OrderId = OrderDetailsCart.OrderHeader.Id,
                    Name = item.StoreItem.Name,
                    Price = item.StoreItem.Price,
                    Count = item.Count
                };

                PurchaseHistory purchaseHistory = new PurchaseHistory
                {
                    ApplicationUserId = claim.Value,
                    Count = item.Count,
                    StoreItemId = item.StoreItemId,
                    PurchaseDate = DateTime.Now,
                };


                OrderDetailsCart.OrderHeader.OrderTotal += (orderDetails.Count * orderDetails.Price) * (1 + SD.SalesTaxPercent);

                _unitOfWork.OrderDetails.Add(orderDetails);
                _unitOfWork.PurchaseHistory.Add(purchaseHistory);
            }

            OrderDetailsCart.OrderHeader.OrderTotal = Convert.ToDouble(String.Format("{0:.##}", OrderDetailsCart.OrderHeader.OrderTotal));

            _unitOfWork.ShoppingCart.RemoveRange(OrderDetailsCart.ListCart);
            HttpContext.Session.SetInt32(SD.ShoppingCart, 0);

            _unitOfWork.Save();


            if(stripeToken != null)
            {
                var options = new ChargeCreateOptions
                {
                    //Amount in cents
                    Amount = Convert.ToInt32(OrderDetailsCart.OrderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Description = "Order ID: " + OrderDetailsCart.OrderHeader.Id,
                    Source = stripeToken
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

                OrderDetailsCart.OrderHeader.TransactionId = charge.Id;
                
                if(charge.Status.ToLower() == "succeeded")
                {
                    //Send a confirmation email
                    OrderDetailsCart.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;

                }

                else
                {
                    OrderDetailsCart.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
                }
            }

            _unitOfWork.Save();
            return RedirectToPage("/User/Cart/OrderConfirmation", new { id = OrderDetailsCart.OrderHeader.Id });

        }
    }
}