using Microsoft.AspNetCore.Mvc;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using NoPoorAfrica.Models.ViewModels;
using System;
using System.Collections.Generic;

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
            List<ArticlesDataTableView> Dt = new List<ArticlesDataTableView>();
            var Articles = _unitOfWork.Article.GetAll();

            foreach (var article in Articles)
            {
                string Thumbnail = _unitOfWork.ArticleFiles.GetFirstImage(article.Id);
                string Path = "";

                if (Thumbnail != null)
                {
                    Path = Thumbnail;
                }

                Dt.Add(new ArticlesDataTableView()
                {
                    Id = article.Id,
                    Body = article.Body,
                    BodyFont = article.BodyFont,
                    PublishDate = article.PublishDate,
                    UpdateDate = article.UpdateDate,
                    ArticleCategoryId = article.ArticleCategoryId,
                    BodyTextSize = article.BodyTextSize,
                    RouteName = article.RouteName,
                    Title = article.Title,
                    Template = article.Template,
                    TitleFont = article.TitleFont,
                    Image = Path
                });
            }

            return Json(new { success = true , data = Dt} );
        }

        // PATCH api/<ArticlesController>/Publish/id
        [HttpPatch]
        [Route("/api/[controller]/Publish")]
        public JsonResult Publish(int id)
        {
            var objFromDb = _unitOfWork.Article.GetFirstOrDefault(i => i.Id == id);

            if (objFromDb.IsPublished == false)
            {
                objFromDb.PublishDate = DateTime.Now;
                objFromDb.IsPublished = true;
                _unitOfWork.Article.Update(objFromDb);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Hide" });
            }
            else
            {
                objFromDb.IsPublished = false;
                _unitOfWork.Article.Update(objFromDb);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Publish" });
            }
        }

        // GET api/<ArticlesController>/PublishStatus/
        [HttpGet]
        [Route("/api/[controller]/PublishStatus")]
        public JsonResult PublishStatus()
        {
            var IdList = _unitOfWork.Article.GetArticleIds();
            var PublishList = _unitOfWork.Article.GetPublishStatus();

            return Json(new { item = IdList.ToArray(), status = PublishList.ToArray() });
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
