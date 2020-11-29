using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoPoorAfrica.DataAccess.Data;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.Pages.User.Store
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<StoreItem> StoreItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }

        public void OnGet()
        {
            StoreItemList = _unitOfWork.StoreItem.GetAll(null, null, "Category,Size");
            CategoryList = _unitOfWork.Category.GetAll();
        }
    }
}
