using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.ViewModels;
using NoPoorAfrica.Utility;

namespace NoPoorAfrica.Pages.Admin.StoreItems
{
    [Authorize(Roles = SD.AdminRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public StoreItemVM StoreItemObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            StoreItemObj = new StoreItemVM
            {
                StoreItem = new Models.Models.StoreItem(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                SizeList = _unitOfWork.Size.GetSizeListForDropDown()
            };

            if(id != null)
            {
                StoreItemObj.StoreItem = _unitOfWork.StoreItem.GetFirstOrDefault(u => u.Id == id);
                if(StoreItemObj == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                if (StoreItemObj.StoreItem.Id != 0)
                {
                    StoreItemObj.StoreItem = _unitOfWork.StoreItem.GetFirstOrDefault(u => u.Id == StoreItemObj.StoreItem.Id);

                    if (StoreItemObj == null)
                    {
                        return NotFound();
                    }

                    StoreItemObj.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
                    StoreItemObj.SizeList = _unitOfWork.Size.GetSizeListForDropDown();
                }

                else
                {
                    StoreItemObj = new StoreItemVM
                    {
                        StoreItem = new Models.Models.StoreItem(),
                        CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                        SizeList = _unitOfWork.Size.GetSizeListForDropDown()
                    };
                }

                return Page();
            }

            if (StoreItemObj.StoreItem.Id == 0) //means a brand new menuItem
            {
                //physically upload and save image
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\storeitems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                //save the string data path
                StoreItemObj.StoreItem.Image = @"\images\menuitems\" + fileName + extension;
                _unitOfWork.StoreItem.Add(StoreItemObj.StoreItem);
            }

            else //update
            {
                var objFromDb = _unitOfWork.StoreItem.Get(StoreItemObj.StoreItem.Id);
                if (files.Count > 0)
                {
                    //physically upload and save image
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\storeitems");
                    var extension = Path.GetExtension(files[0].FileName);
                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\')); //trim off forward slash

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    //save the string data path
                    StoreItemObj.StoreItem.Image = @"\images\storeitems\" + fileName + extension;
                }

                else
                {
                    StoreItemObj.StoreItem.Image = objFromDb.Image;
                }

                _unitOfWork.StoreItem.Update(StoreItemObj.StoreItem);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
