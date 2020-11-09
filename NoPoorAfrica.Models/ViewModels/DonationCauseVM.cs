using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.Models.ViewModels
{
    public class DonationCauseVM
    {
        public DonationCause DonationCause { get; set; }
        public IEnumerable<SelectListItem> DonationCauseCategoryList { get; set; }
    }
}
