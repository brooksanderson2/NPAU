using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;

namespace NoPoorAfrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationCauseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DonationCauseController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.DonationCause.GetAll(null, null, null) });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            try
            {

                var objFromDb = _unitOfWork.DonationCause.GetFirstOrDefault(u => u.Id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                //physically remove image (if it exists)
                var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _unitOfWork.DonationCause.Remove(objFromDb);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
