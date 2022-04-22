using Domains.Models;
using Domains.SearchModels;
using Repository.Interfaces.Common;
using Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface ICategoryService : IService<Category, BaseSearch>
    {
        //List<Category> GetCategoryByMasterCategoryId(int Id, BaseSearch search);
    }
}
