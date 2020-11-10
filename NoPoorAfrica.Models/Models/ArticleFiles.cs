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
        [ForeignKey("ArticleId")]
        public virtual int ArticleId { get; set; }

        /// <summary>
        /// Path to file
        /// </summary>
        [Required]
        public string File { get; set; }
    }
}
