using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendingWiresController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PendingWiresController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.PendingWire.GetAll(null, null, "DonationCause") });
        }




        [HttpPost("{id}")]
        public IActionResult Post(int id)
        {
            try
            {

                DonationDetails obj = new DonationDetails();
                var objFromDb = _unitOfWork.PendingWire.GetFirstOrDefault(u => u.Id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while approving" });
                }
                obj.UserId = objFromDb.UserId;
                obj.DonationCauseId = objFromDb.DonationCauseId;
                obj.DonorName = objFromDb.DonorName;
                obj.DonationTotal = objFromDb.DonationTotal;
                obj.PaymentStatus = objFromDb.PaymentStatus;
                obj.Email = objFromDb.Email;
                obj.Comments = objFromDb.Comments;
                obj.FollowUp = objFromDb.FollowUp;
                obj.TransactionId = objFromDb.Id.ToString();
                obj.DonationDate = objFromDb.DonationDate;
                obj.DonationCause = objFromDb.DonationCause;

                _unitOfWork.PendingWire.Remove(objFromDb);
                _unitOfWork.DonationDetails.Add(obj);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error while approving" });
            }
            return Json(new { success = true, message = "Approval Successful" }); 
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            try
            {

                var objFromDb = _unitOfWork.PendingWire.GetFirstOrDefault(u => u.Id == id);
                if (objFromDb == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }

                _unitOfWork.PendingWire.Remove(objFromDb);
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
