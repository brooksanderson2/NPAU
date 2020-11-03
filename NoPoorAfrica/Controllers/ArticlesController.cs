using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoPoorAfrica.DataAccess.Data.Repository;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArticlesController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<ArticlesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Article.GetAll() });
        }

        // GET api/<ArticlesController>/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Article.GetFirstOrDefault(u => u.Id == id);

            if (objFromDb == null)
                return Json(new { success = false, message = "Error while deleting." });

            _unitOfWork.Article.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful." });
        }
    }
}
