using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using NoPoorAfrica.Models.ViewModels;

namespace NoPoorAfrica.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public Article DetailedArticle { get; set; }
        public bool DetailedView { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ArticleFiles> ImageGallery { get; set; }
        public List<ArticlesViewModel> ArticlesList { get; set; }

        public void OnGet(string id)
        {
            DetailedView = false;

            if (id == null) 
            {
                //If no article is selected in url, show recent articles
                ArticlesList = new List<ArticlesViewModel>();
                var Articles = _unitOfWork.Article.GetAll(i => i.IsPublished == true).OrderByDescending(i => i.PublishDate);

                foreach (var article in Articles)
                {
                    string Thumbnail = null;
                    string Path = "";

                    try
                    {
                        Thumbnail = _unitOfWork.ArticleFiles.GetFirstImage(article.Id);
                    }
                    catch { }

                    if (Thumbnail != null)
                    {
                        Path = Thumbnail;
                    }

                    ArticlesList.Add(new ArticlesViewModel()
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
            }
            else
            {
                //Show article specified in url {id}
                //Articles?id=articleRoute


                DetailedView = true;
                DetailedArticle = _unitOfWork.Article.GetFirstOrDefault(i => i.RouteName == id);

                //Article is not published. Return not found.
                if (DetailedArticle.IsPublished == false)
                    Redirect("./");

                ImageGallery = _unitOfWork.ArticleFiles.GetAll(f => f.ArticleId == DetailedArticle.Id);

            }
        }
    }
}
