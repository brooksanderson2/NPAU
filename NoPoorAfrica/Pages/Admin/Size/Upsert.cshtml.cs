using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;

namespace NoPoorAfrica.Pages.Admin.Size
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.Models.Size SizeObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            SizeObj = new Models.Models.Size();

            if (id != null)
            {
                SizeObj = _unitOfWork.Size.GetFirstOrDefault(u => u.Id == id);
                if (SizeObj == null)
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

            if (SizeObj.Id == 0) //means a brand new Size
            {
                _unitOfWork.Size.Add(SizeObj);
            }

            else
            {
                _unitOfWork.Size.Update(SizeObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
