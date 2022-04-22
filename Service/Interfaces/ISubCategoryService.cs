using Domains.DTO;
using Domains.Models;
using Domains.SearchModels;
using Repository.Interfaces.Common;
using Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface ISubCategoryService : IService<SubCategory, BaseSearch>
    {
        IEnumerable<SubCategory> GetByParentId(int Id);
    }
}
