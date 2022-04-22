using Domains.Models;
using Repository.Context;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private MyKidsStoreDbContext _context;
        public CategoryRepository(MyKidsStoreDbContext context) : base(context)
        {


            _context = context;
        }
    }
}
