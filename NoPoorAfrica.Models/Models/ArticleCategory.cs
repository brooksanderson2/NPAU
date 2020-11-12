using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class ArticleCategory
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Category description
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}