using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class StoreItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price should be greater than $1")]
        public double Price { get; set; }

        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }

       [Display(Name = "Size")]
      public int SizeId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

       [ForeignKey("SizeId")]
        public virtual Size Size { get; set; }

    }
}
