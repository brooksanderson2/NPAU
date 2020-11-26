using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.Models.ViewModels
{
    public class ArticleUpsertVM
    {
        public Article Article { get; set; }
        public IEnumerable<ArticleFiles> Thumbnails { get; set; }
    }
}
