using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using NoPoorAfrica.Utility;

namespace NoPoorAfrica.Pages.Admin.Articles
{
    [Authorize(Roles = SD.AdminRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Article ArticleObj { get; set; }
        [BindProperty]
        public IEnumerable<SelectListItem> ArticleCategoryList { get; set; }
        [BindProperty]
        public IEnumerable<string> ThumbnailList { get; set; }
        public IEnumerable<string> UploadList { get; set; }

        public IActionResult OnGet(int? id)
        {
            ArticleObj = new Article();
            ArticleCategoryList = _unitOfWork.ArticleCategory.GetArticleCategoryList();

            //Check if it's a new article
            if (id != null)
            {
                ArticleObj = _unitOfWork.Article.GetFirstOrDefault(u => u.Id == id);
                if (ArticleObj == null)
                {
                    return NotFound();
                }

                ThumbnailList = _unitOfWork.ArticleFiles.GetByArticleAscending(ArticleObj.Id);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            UploadList = Enumerable.Empty<string>();

            //Set times
            if (ArticleObj.Id == 0)
            {
                ArticleObj.PublishDate = DateTime.Now;

                //Set route
                if (_unitOfWork.Article.GetAll(i => i.RouteName == HttpUtility.UrlEncode(ArticleObj.Title) && i.Id != ArticleObj.Id).Count() == 0)
                //Check if route already exists based on title, so that two routes cannot be the same.
                {
                    ArticleObj.RouteName = HttpUtility.UrlEncode(ArticleObj.Title);
                }
                else
                {
                    ArticleObj.RouteName = HttpUtility.UrlEncode((ArticleObj.PublishDate.ToShortDateString() + "-" + ArticleObj.Title));
                }
            }

            ArticleObj.UpdateDate = DateTime.Now;

            if (!ModelState.IsValid)
                return Page();

            ThumbnailList = _unitOfWork.ArticleFiles.GetByArticleAscending(ArticleObj.Id);


            if (ArticleObj.Id == 0)
            {
                _unitOfWork.Article.Add(ArticleObj);

                _unitOfWork.Save();
            }
            else //Article already exists - user is updating it.
            {
                _unitOfWork.Article.Update(ArticleObj);
                _unitOfWork.Save();
            }

            //Add uploaded images to repository model
            foreach (var item in HttpContext.Request.Form.Files)
            {
                //Upload and save image
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, @"images\ArticleImages\");
                var extension = Path.GetExtension(item.FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    item.CopyTo(fileStream);
                }

                ArticleFiles UploadedImage = new ArticleFiles
                {
                    ArticleId = ArticleObj.Id,
                    FilePath = "\\images\\ArticleImages\\" + fileName + extension,
                    OriginalName = item.FileName,
                    Position = _unitOfWork.ArticleFiles.GetLowestAvailablePosition(ArticleObj.Id)
                };

                _unitOfWork.ArticleFiles.Add(UploadedImage);
                _unitOfWork.Save();
            }
            return LocalRedirect("~/Admin/Articles/Upsert?id="+ArticleObj.Id);
        }
    }
}

