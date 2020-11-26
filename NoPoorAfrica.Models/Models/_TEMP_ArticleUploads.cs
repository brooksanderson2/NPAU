using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    /// <summary>
    /// Used to hold uploaded images by the user during Article creation. Saves state of images so that a dummy article doesn't have to be created.
    /// Should be deleted when article is created.
    /// </summary>
    public class _TEMP_ArticleUploads
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SessionId { get; set; }
        
        [Required]
        public string FilePath { get; set; }
    }
}
