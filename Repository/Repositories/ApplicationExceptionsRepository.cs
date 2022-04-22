using Domain.Models.Common;
using Repository.Context;
using Repository.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories.Common
{
    public class ApplicationExceptionsRepository : IApplicationExceptionsRepository
    {
        #region [Context]
        protected MyKidsStoreDbContext Context;
        #endregion

        public ApplicationExceptionsRepository(MyKidsStoreDbContext context)
        {
            Context = context;
        }

        public ApplicationExceptions WriteException(ApplicationExceptions ex)
        {
            Context.Set<ApplicationExceptions>().Add(ex);
            Context.SaveChanges();
            Context.Entry(ex).GetDatabaseValues();

            return ex;
        }
    }
}
