using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.Models.ViewModels
{
    public class StoreItemVM
    {
        public StoreItem StoreItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
