using NoPoorAfrica.Models.Models;
using System.Collections.Generic;

namespace NoPoorAfrica.Models.ViewModels
{
    public class ArticlesTemplateViewModel : Article
    {
        public List<ArticleFiles> ImageGallery { get; set; }
    }
}
