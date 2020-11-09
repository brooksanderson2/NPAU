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
        //private readonly IUnitOfWork _unitOfWork;

        //public IndexModel(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        //public IEnumerable<StoreItem> StoreItemList { get; set; }
        //public IEnumerable<Category> CategoryList { get; set; }

        //public void OnGet()
        //{
        //    StoreItemList = _unitOfWork.StoreItem.GetAll(null, null, "Category");
        //    CategoryList = _unitOfWork.Category.GetAll();
        //}


        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<StoreItem> StoreItem { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            PriceSort = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<StoreItem> storeItemIQ = from s in _context.StoreItem select s;
            //IQueryable<Genre> genreIQ = from q in _context.Genre select q;
            //IQueryable<Director> directorIQ = from d in _context.Director select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                storeItemIQ = storeItemIQ.Where(s => s.Name.Contains(searchString));
                //genreIQ = genreIQ.Where(q => q.Name.Contains(searchString));
                //directorIQ = directorIQ.Where(d => d.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Name_desc":
                    storeItemIQ = storeItemIQ.OrderByDescending(s => s.Name);
                    break;
                case "Price_desc":
                    storeItemIQ = storeItemIQ.OrderByDescending(s => s.Price);
                    break;

                default:
                    storeItemIQ = storeItemIQ.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            StoreItem = await PaginatedList<StoreItem>.CreateAsync(
                storeItemIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }


    }
}
