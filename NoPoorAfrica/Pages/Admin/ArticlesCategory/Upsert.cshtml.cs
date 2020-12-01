using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;

namespace NoPoorAfrica.Pages.Admin.ArticlesCategory
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Models.Models.ArticleCategory ArticleCategoryObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            ArticleCategoryObj = new Models.Models.ArticleCategory();

            if (id != null)
            {
                ArticleCategoryObj = _unitOfWork.ArticleCategory.GetFirstOrDefault(u => u.Id == id);
                if (ArticleCategoryObj == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ArticleCategoryObj.Id == 0) //New Object
            {
                _unitOfWork.ArticleCategory.Add(ArticleCategoryObj);
            }

            else
            {
                _unitOfWork.ArticleCategory.Update(ArticleCategoryObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
