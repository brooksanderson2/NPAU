using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using NoPoorAfrica.Utility;

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
        public List<string> ThumbnailList { get; private set; }

        public IActionResult OnGet(int? id)
        {
            ArticleObj = new Article();

            //Check if it's a new article
            if (id != null)
            {
                ArticleObj = _unitOfWork.Article.GetFirstOrDefault(u => u.Id == id);
                if (ArticleObj == null)
                {
                    return NotFound();
                }

                PopulateThumbnailList();
                //TempData.Put<IEnumerable<ArticleFiles>>("ThumbnailList", ThumbnailList);
            }
            else
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            int? SessionId = HttpContext.Session.GetInt32("ArticleUploads");

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
            ArticleObj.ArticleCategoryId = 1;

            if (!ModelState.IsValid)
                return Page();


            PopulateThumbnailList();
            //ThumbnailList = TempData.Get<IEnumerable<ArticleFiles>>("ThumbnailList");

            if (ArticleObj.Id == 0)
            {
                _unitOfWork.Article.Add(ArticleObj);

                _unitOfWork.Save();

                //Add images after generating an ArticleId.
                try
                {
                    if (ThumbnailList != null && SessionId != null)
                    {
                        //Fix file relationships that were given an ArticleId of 0.
                        var TempFiles = _unitOfWork.TempUploads.GetAll(i => i.SessionId == SessionId);

                        foreach (var TempFile in TempFiles)
                        {
                            ArticleFiles file = new ArticleFiles
                            {
                                ArticleId = ArticleObj.Id,
                                FilePath = TempFile.FilePath
                            };

                            _unitOfWork.ArticleFiles.Add(file);
                            _unitOfWork.TempUploads.Remove(TempFile);
                        }
                    }
                }
                catch
                {

                }
            }
            else //Article already exists - user is updating it.
            {
                var TempFiles = _unitOfWork.TempUploads.GetAll(i => i.SessionId == SessionId);
                var CurrentFiles = _unitOfWork.ArticleFiles.GetAll(i => i.ArticleId == ArticleObj.Id);
                List<string> CurrentPaths = new List<string>();

                foreach (var File in CurrentFiles)
                {
                    CurrentPaths.Add(File.FilePath);
                }

                foreach (var TempFile in TempFiles)
                {
                    if (!CurrentPaths.Contains(TempFile.FilePath))
                    {
                        ArticleFiles file = new ArticleFiles
                        {
                            ArticleId = ArticleObj.Id,
                            FilePath = TempFile.FilePath
                        };

                        _unitOfWork.ArticleFiles.Add(file);
                        _unitOfWork.TempUploads.Remove(TempFile);
                    }
                }

                _unitOfWork.Article.Update(ArticleObj);
            }

            _unitOfWork.Save();

            HttpContext.Session.SetInt32("ArticleUploads", -1);

            return RedirectToPage("./Index");
        }

        public IActionResult OnPostUpload(int Id, List<IFormFile> files)
        {
            ArticleObj.Id = Id;

            string folderName;
            string webRootPath;
            string newPath;

            if (files != null && files.Count > 0)
            {
                folderName = @"images\ArticleImages\";
                webRootPath = _webHostEnvironment.WebRootPath;
                newPath = Path.Combine(webRootPath, folderName);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
            }
            else
            {
                return this.Content("Fail");
            }

            // Set a temporary session id to save the files that were uploaded. They cannot be put into the ArticleFiles
            // table yet because of the foreign key restraint on the Article ID that does not exist yet. Once the
            // Article ID exists (on Post), move the items from the temporary table to the ArticleFiles table and
            // assign the ID.
            if (ArticleObj.Id == 0)
            {
                Random rnd = new Random();
                int randSession = rnd.Next(0, Int32.MaxValue);
                int? SessionId = HttpContext.Session.GetInt32("ArticleUploads");

                if (SessionId == null || SessionId == -1)
                {
                    HttpContext.Session.SetInt32("ArticleUploads", randSession);
                }

                List<_TEMP_ArticleUploads> list = new List<_TEMP_ArticleUploads>();

                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        string dbPath = @"\" + folderName + fileName;

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }

                        _TEMP_ArticleUploads Relationship = new _TEMP_ArticleUploads()
                        {
                            FilePath = dbPath,
                            SessionId = randSession
                        };

                        list.Add(Relationship);
                    }
                }

                return this.Content("Success");
            }
                
            // The Article is being updated, not created. The Article ID already exists
            // We do not have to store the files in the temporary table.
            else
            {
                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        string dbPath = @"\" + folderName + fileName;

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }

                        ArticleFiles Relationship = new ArticleFiles()
                        {
                            ArticleId = ArticleObj.Id,
                            FilePath = dbPath
                        };

                        _unitOfWork.ArticleFiles.Add(Relationship);
                    }
                }

                _unitOfWork.Save();
                return this.Content("Success");
            }
        }

        public void PopulateThumbnailList()
        {
            ThumbnailList = new List<string>();

            int? SessionId = HttpContext.Session.GetInt32("ArticleUploads");

            var ArticleImages = _unitOfWork.ArticleFiles.GetAll(i => i.ArticleId == ArticleObj.Id);
            foreach (var item in ArticleImages)
            {
                ThumbnailList.Add(item.FilePath);
            }

            //Generally should not have a session id when using GET, but checks for any pending uploads just in case.
            if (SessionId != null && SessionId != -1)
            {
                var PendingUploads = _unitOfWork.TempUploads.GetAll(i => i.SessionId == SessionId);

                foreach (var item in PendingUploads)
                {
                    ThumbnailList.Add(item.FilePath);
                }
            }
        }
    }
}

