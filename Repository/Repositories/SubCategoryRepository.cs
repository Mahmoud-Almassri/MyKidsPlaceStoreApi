using Domains.Models;
using Repository.Context;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        private MyKidsStoreDbContext _context;
        public SubCategoryRepository(MyKidsStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
