using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        public string UserId { get; set; }

        ////Newly added in and will be used with a logged in customer
        //[Display(Name = "Order Name")]
        //public string OrderName { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double OrderTotal { get; set; }

        [Required]
        [NotMapped]
        public DateTime PurchaseDate { get; set; }

        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string Comments { get; set; }
        public string TransactionId { get; set; }

        
    }
}
