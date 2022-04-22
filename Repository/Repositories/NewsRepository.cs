using Domains.Models;
using Repository.Context;
using Repository.Repositories.Common;

namespace Repository.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        private MyKidsStoreDbContext _context;
        public NewsRepository(MyKidsStoreDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
