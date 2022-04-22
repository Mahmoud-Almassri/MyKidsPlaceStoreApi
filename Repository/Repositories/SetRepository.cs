using Domains.Models;
using Repository.Context;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class SetRepository : Repository<Set>, ISetRepository
    {
        private MyKidsStoreDbContext _context;
        public SetRepository(MyKidsStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
