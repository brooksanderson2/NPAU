
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.Models.Models;


namespace NoPoorAfrica.Models.ViewModels
{
    public class OrderDetailsCartVM
    {
        public OrderHeader OrderHeader { get; set; }
        public List<ShoppingCart> ListCart { get; set; }
    }
}
