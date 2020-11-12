﻿using Microsoft.AspNetCore.Mvc.Rendering;
using NoPoorAfrica.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPoorAfrica.DataAccess.Data.Repository.IRepository
{
    public interface IArticleFilesRepository : IRepository<ArticleFiles>
    {
        IEnumerable<SelectListItem> GetArticleFilesList();

        void Update(ArticleFiles articleFiles);
    }
}