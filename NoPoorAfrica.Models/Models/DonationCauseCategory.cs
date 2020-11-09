using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class DonationCauseCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
