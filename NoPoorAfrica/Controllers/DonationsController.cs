using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Utility;

namespace NoPoorAfrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.AdminRole)]
    public class DonationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DonationsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.DonationDetails.GetAll(null, null, "DonationCause") });
        }
    }
}
