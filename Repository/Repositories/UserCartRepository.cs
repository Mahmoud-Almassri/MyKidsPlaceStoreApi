using Domains.Models;
using Repository.Context;
using Repository.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class UserCartRepository : Repository<UserCart>, IUserCartRepository
    {
        private MyKidsStoreDbContext _context;
        public UserCartRepository(MyKidsStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
