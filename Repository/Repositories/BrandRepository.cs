using Domains.Models;
using Repository.Context;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private MyKidsStoreDbContext _context;
        public BrandRepository(MyKidsStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
