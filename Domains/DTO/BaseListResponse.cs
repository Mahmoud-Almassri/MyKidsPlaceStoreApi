using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.DTO
{
    public class BaseListResponse<TEntity>
    {
        public List<TEntity> entities { get; set; }
        public int TotalCount { get; set; }
}
}
