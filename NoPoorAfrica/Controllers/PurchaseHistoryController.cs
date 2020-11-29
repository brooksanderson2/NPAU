using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Utility;

namespace NoPoorAfrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseHistoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvionment;

        public PurchaseHistoryController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvionment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (User.IsInRole(SD.AdminRole))
            {
                return Json(new { data = _unitOfWork.PurchaseHistory.GetAll(null, null, "StoreItem,ApplicationUser") });
            }

            else
            {
                return Json(new { data = _unitOfWork.PurchaseHistory.GetAll(c => c.ApplicationUserId == claim.Value, null, "StoreItem,ApplicationUser") });
            }
        }
    }
}
