using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class PurchaseHistory
    {
           
        public int Id { get; set; }

        public PurchaseHistory()
        {
            Count = 1;
        }

        [Range(1, 100, ErrorMessage = "Please select a count between 1 and 100")]
        public int Count { get; set; }
        public double Total { get; set; }

        public string ApplicationUserId { get; set; }

        [Required]
        [Display(Name = "Store Item Id")]
        public int StoreItemId { get; set; }

        [Required]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [ForeignKey("StoreItemId")]
        public virtual StoreItem StoreItem { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
