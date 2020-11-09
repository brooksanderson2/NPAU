using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;

namespace NoPoorAfrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationCauseCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DonationCauseCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.DonationCauseCategory.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.DonationCauseCategory.GetFirstOrDefault(u => u.Id == id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.DonationCauseCategory.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
