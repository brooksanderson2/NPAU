using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoPoorAfrica.Models.Models
{
    public class ArticleFiles
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key to Article
        /// </summary>
        [Required]
        public virtual int ArticleId { get; set; }

        /// <summary>
        /// Path to file
        /// </summary>
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] File { get; set; }

        /// <summary>
        /// Object of Article FK using ArticleId
        /// </summary>
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}
