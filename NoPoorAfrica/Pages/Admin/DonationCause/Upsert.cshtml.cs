using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NoPoorAfrica.DataAccess.Data.Repository.IRepository;
using NoPoorAfrica.Models.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoPoorAfrica.Models.ViewModels;

namespace NoPoorAfrica.Pages.Admin.DonationCause
{
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
        public DonationCauseVM DonationCauseObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            DonationCauseObj = new DonationCauseVM
            {
                DonationCause = new Models.Models.DonationCause(),
                DonationCauseCategoryList = _unitOfWork.DonationCauseCategory.GetDonationCauseCategoryListForDropDown()
            };

            if (id != null) //edit menu item
            {
                DonationCauseObj.DonationCause = _unitOfWork.DonationCause.GetFirstOrDefault(u => u.Id == id);
                if (DonationCauseObj == null)
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
                return Page();
            }

            if (DonationCauseObj.DonationCause.Id == 0) //means new menu item
            {
                //Physically upload and save image
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Images\DonationCauses");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                //save the string data path
                DonationCauseObj.DonationCause.Image = @"\Images\DonationCauses\" + fileName + extension;

                _unitOfWork.DonationCause.Add(DonationCauseObj.DonationCause);
            }

            else //update
            {
                var objFromDb = _unitOfWork.DonationCause.Get(DonationCauseObj.DonationCause.Id);
                if (files.Count > 0)
                {
                    //Physically upload and save image
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\DonationCauses");
                    var extension = Path.GetExtension(files[0].FileName);


                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    //save the string data path
                    DonationCauseObj.DonationCause.Image = @"\Images\DonationCauses\" + fileName + extension;
                }
                else
                {
                    DonationCauseObj.DonationCause.Image = objFromDb.Image;
                }
                _unitOfWork.DonationCause.Update(DonationCauseObj.DonationCause);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
