using Microsoft.AspNetCore.Mvc;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using NoPoorAfrica.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoPoorAfrica.Pages.Components.ArticleTemplates
{
    public class TemplateControlViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public TemplateControlViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke(string Template, Article Article)
        {
            ArticlesTemplateViewModel VM = new ArticlesTemplateViewModel()
            {
                Id = Article.Id,
                Body = Article.Body,
                BodyFont = Article.BodyFont,
                PublishDate = Article.PublishDate,
                UpdateDate = Article.UpdateDate,
                ArticleCategoryId = Article.ArticleCategoryId,
                BodyTextSize = Article.BodyTextSize,
                RouteName = Article.RouteName,
                Title = Article.Title,
                Template = Article.Template,
                TitleFont = Article.TitleFont,
                ImageGallery = _unitOfWork.ArticleFiles.GetAll(i => i.ArticleId == Article.Id).ToList()
            };

            return View(Template, VM);
        }
    }
}
