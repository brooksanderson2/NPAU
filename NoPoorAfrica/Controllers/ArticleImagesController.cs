using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using NoPoorAfrica.DataAccess.Data.Repository;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleImagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArticleImagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<ArticleImagesController/ArticleId>
        [HttpGet("{ArticleId}")]
        public IActionResult Get(int ArticleId)
        {
            return Json( new { data = _unitOfWork.ArticleFiles.GetByArticleWithPosition(ArticleId) });
        }
        
        // PATCH: api/<ArticleImagesController>/Left/ArticleId?Path
        [HttpPatch]
        [Route("/api/[controller]/Left")]
        public JsonResult Left(int ArticleId, string Path)
        {
            var item = _unitOfWork.ArticleFiles.GetFirstOrDefault(i => i.FilePath == HttpUtility.UrlDecode(Path));
            if (item != null)
            {
                if (item.Position > 0)
                {
                    try
                    {
                        ArticleFiles ItemToSwap = _unitOfWork.ArticleFiles.GetFirstOrDefault(i => i.Position == item.Position - 1 && i.ArticleId == ArticleId);
                        if (ItemToSwap != null)
                        {
                            ItemToSwap.Position++;
                            _unitOfWork.ArticleFiles.Update(ItemToSwap);
                        }

                        item.Position--;
                        _unitOfWork.ArticleFiles.Update(item);
                        _unitOfWork.Save();

                        return Json(new { success = true, message = "Position updated!", target1 = item.FilePath, target2 = ItemToSwap.FilePath });
                    }
                    catch
                    {
                        return Json( new { success = false, message = "Error updating position." });
                    }
                }
                else return Json( new { success = false, message = "Item is at first position."});
            }
            else //Image with FilePath == Path has not been posted to the server - this code really shouldn't run
            {
                return Json( new { success = false, message = "Item is not in the model. Post the image to the server first." });
            }
        }

        // PATCH: api/<ArticleImagesController>/Right/ArticleId?Path
        [HttpPatch]
        [Route("/api/[controller]/Right")]
        public JsonResult Right(int ArticleId, string Path)
        {
            var item = _unitOfWork.ArticleFiles.GetFirstOrDefault(i => i.FilePath == HttpUtility.UrlDecode(Path));
            int end = _unitOfWork.ArticleFiles.GetLastPosition(ArticleId);
            if (item != null)
            {
                if (item.Position < end)
                {
                    try
                    {
                        ArticleFiles ItemToSwap = _unitOfWork.ArticleFiles.GetFirstOrDefault(i => i.Position == item.Position + 1 && i.ArticleId == ArticleId);
                        if (ItemToSwap != null)
                        {
                            ItemToSwap.Position--;
                            _unitOfWork.ArticleFiles.Update(ItemToSwap);
                        }

                        item.Position++;
                        _unitOfWork.ArticleFiles.Update(item);
                        _unitOfWork.Save();

                        return Json( new { success = true, message = "Position updated!", target2 = item.FilePath, target1 = ItemToSwap.FilePath });
                    }
                    catch
                    {
                        return Json( new { success = false, message = "Error updating position." });
                    }
                }
                else return Json( new { success = false, message = "Item is at first position." });
            }
            else //Image with FilePath == Path has not been posted to the server - this code really shouldn't run
            {
                return Json( new { success = false, message = "Item is not in the model. Post the image to the server first." });
            }
        }

        // GET api/<ArticleImagesController>/DeleteImage/id
        [HttpDelete]
        [Route("/api/[controller]/Delete")]
        public JsonResult DeleteImage(int ArticleId, string Path)
        {
            var item = _unitOfWork.ArticleFiles.GetFirstOrDefault(i => i.FilePath == HttpUtility.UrlDecode(Path));
            if (item != null)
            {
                try
                {
                    int Pos = item.Position;
                    _unitOfWork.ArticleFiles.Remove(item);

                    var Collection = _unitOfWork.ArticleFiles.GetAll(i => i.ArticleId == ArticleId);
                    foreach (var image in Collection)
                    {
                        if (image.Position > Pos)
                        {
                            image.Position--;
                            _unitOfWork.ArticleFiles.Update(image);
                        }
                    }

                    _unitOfWork.Save();

                    return Json( new { success = true, message = "Delete successful." });
                }
                catch
                {
                    return Json( new { success = false, message = "Delete failed." });
                }
            }

            return Json( new { success = false, message = "Delete failed. Item does not exist." });
        }
    }
}
