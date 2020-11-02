using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Size")]
        public string Name { get; set; }
    }
}
