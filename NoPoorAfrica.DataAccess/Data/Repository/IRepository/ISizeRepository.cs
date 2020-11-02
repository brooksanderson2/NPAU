using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.Models.Models;

namespace NoPoorAfrica.DataAccess.Data.Repository.IRepository
{
    public interface ISizeRepository : IRepository<Size>
    {
        IEnumerable<SelectListItem> GetSizeListForDropDown();

        void Update(Size size);
    }
}
