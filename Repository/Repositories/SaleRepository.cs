using Domains.Models;
using Repository.Context;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        private MyKidsStoreDbContext _context;
        public SaleRepository(MyKidsStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
