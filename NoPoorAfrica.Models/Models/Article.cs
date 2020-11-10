﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NoPoorAfrica.Models.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Title of the article.
        /// </summary>
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Font family for the title.
        /// </summary>
        [Display(Name = "Title Font")]
        public string TitleFont { get; set; } = "Arial"; // TODO: What is going to be the default font family?
                                                         //       Maybe the template sets the default? -- Brooks

        /// <summary>
        /// Body content of the article formatted with TinyMCE.
        /// </summary>
        [AllowNull]
        [Display(Name = "Body")]
        public string Body { get; set; }

        /// <summary>
        /// Font family for the body content.
        /// </summary>
        [Display(Name = "Body Font")]
        public string BodyFont { get; set; } = "Arial"; // TODO: What is going to be the default font family?
                                                        //       Maybe the template sets the default? -- Brooks

        /// <summary>
        /// Text size for the body content.
        /// </summary>
        [Display(Name = "Body Text Size")]
        public float BodyTextSize { get; set; } = 12; // TODO: What is going to be the default text size?
                                                      //       Maybe the template sets the default? -- Brooks

        /// <summary>
        /// Template to format the article page.
        /// May be wise to make this a foreign key to a template model - may be unnecessary. 
        /// </summary>
        [Display(Name = "Article Template")]
        [Required]
        public string Template { get; set; } = "template_1";

        /// <summary>
        /// Date of article
        /// </summary>
        [Required]
        [Display(Name = "Date")]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Date of most recent update to the article. Will show as PublishDate if never updated.
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Used for routing inside the Article page controller, since the page is dynamic.
        /// </summary>
        [Required]
        public string RouteName { get; set; }

        /// <summary>
        /// Foreign key to ArticleCategory
        /// </summary>
        [ForeignKey("ArticleCategory")]
        public virtual ArticleCategory ArticleCategory { get; set; }
    }
}
