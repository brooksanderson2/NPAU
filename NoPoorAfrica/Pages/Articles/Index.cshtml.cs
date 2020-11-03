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
        const int PAGESIZE = 3;

        private readonly IUnitOfWork _unitOfWork;

        public ArticlesVM ArticlesList { get; set; }
        public Article DetailedArticle { get; set; }
        public bool DetailedView { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(string id)
        {
            DetailedView = false;
            ArticlesList = new ArticlesVM();

            if (id == null) 
            {
                //If no article is selected in url, show recent articles

                ArticlesList.Articles = _unitOfWork.Article.GetAll();
            }
            else
            {
                //Show article specified in url {id}
                //Articles?id=articleRoute

                DetailedView = true;

                DetailedArticle = _unitOfWork.Article.GetFirstOrDefault(i => i.RouteName == id);
            }
        }
    }
}
