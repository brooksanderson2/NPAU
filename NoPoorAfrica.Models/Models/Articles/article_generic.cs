using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NoPoorAfrica.Models.Articles
{
    public class Article_Generic
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }


    }
}
