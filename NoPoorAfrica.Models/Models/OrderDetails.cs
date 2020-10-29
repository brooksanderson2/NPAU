using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
   public class OrderDetails
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        [Required]
        public int StoreItemId { get; set; }

        [ForeignKey("StoreItemId")]
        public virtual StoreItem StoreItem { get; set; }

        public int Count { get; set; }
        public string Name { get; set; }
        
        [Required]
        public double Price { get; set; }
    }
}
