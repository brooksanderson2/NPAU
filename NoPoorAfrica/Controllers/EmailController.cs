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
    public class EmailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public EmailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        [HttpGet]
        public IActionResult Get()
        {
            
                return Json(new { data = _unitOfWork.OrderHeader.GetAll() });

        }
    }
}
