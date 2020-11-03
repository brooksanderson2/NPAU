using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.Pages.Admin.Articles
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Article ArticleObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            ArticleObj = new Article();

            if (id != null)
            {
                ArticleObj = _unitOfWork.Article.GetFirstOrDefault(u => u.Id == id);
                if (ArticleObj == null)
                {
                    return NotFound();
                }
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
            }
            else
            {
                _unitOfWork.Article.Update(ArticleObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}

