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
        /// Path to file - Uses a GUID to avoid crossover.
        /// </summary>
        [Required]
        public string FilePath { get; set; }

        /// <summary>
        /// The name of the file when it was uploaded
        /// </summary>
        [Required]
        public string OriginalName { get; set; }

        /// <summary>
        /// Position in article. 0 is first.
        /// </summary>
        [Required]
        public int Position { get; set; }

        /// <summary>
        /// Object of Article FK using ArticleId
        /// </summary>
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}
