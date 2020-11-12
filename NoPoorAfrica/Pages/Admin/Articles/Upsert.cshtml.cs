using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.Pages.Admin.Articles
{
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

        public IEnumerable<ArticleFiles> ThumbnailList { get; set; }

        public IActionResult OnGet(int? id)
        {
            ArticleObj = new Article();
            ThumbnailList = new List<ArticleFiles>();

            //Check if it's a new article
            if (id != null)
            {
                ArticleObj = _unitOfWork.Article.GetFirstOrDefault(u => u.Id == id);
                if (ArticleObj == null)
                {
                    return NotFound();
                }
            }

            //Get thumbnails of existing pictures for article
            if (id != null)
            {
                ThumbnailList = _unitOfWork.ArticleFiles.GetAll(f => f.ArticleId == id);
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (ArticleObj.Id == 0) 
            {
                _unitOfWork.Article.Add(ArticleObj);

                _unitOfWork.Save();

                //Fix file relationships that were given an ArticleId of 0.
                foreach(ArticleFiles file in ThumbnailList)
                {
                    file.ArticleId = ArticleObj.Id;
                }

                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.Article.Update(ArticleObj);
                _unitOfWork.Save();
            }
            return RedirectToPage("./Index");
        }

        public IActionResult OnPostUpload(List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                string folderName = "ArticleImages";
                string webRootPath = _webHostEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }

                        ArticleFiles Relationship = new ArticleFiles()
                        {
                            ArticleId = ArticleObj.Id,
                            FilePath = fullPath
                        };

                        _unitOfWork.ArticleFiles.Add(Relationship);

                        ThumbnailList.Append(Relationship);
                    }
                }

                _unitOfWork.Save();
                return this.Content("Success");
            }
            return this.Content("Fail");

        }
    }
}

