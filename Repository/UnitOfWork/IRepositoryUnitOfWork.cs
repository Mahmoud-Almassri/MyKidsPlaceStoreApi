
using Repository.Interfaces;
using Repository.Interfaces.Common;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.UnitOfWork
{
    public interface IRepositoryUnitOfWork : IDisposable
    {
        Lazy<IApplicationExceptionsRepository> ApplicationExceptions { get; set; }
        Lazy<IOrderRepository> Order { get; set; }
         Lazy<IItemImagesRepository> ItemImages { get; set; }
         Lazy<IBrandRepository> Brand { get; set; }
         Lazy<IRoleRepository> Roles { get; set; }
         Lazy<IUserRoleRepository> UserRole { get; set; }
         Lazy<ICartItemRepository> CartItem { get; set; }
         Lazy<IItemRepository> Item { get; set; }
         Lazy<ICategoryRepository> Category { get; set; }
         Lazy<ISubCategoryRepository> SubCategory { get; set; }
         Lazy<ISaleRepository> Sale { get; set; }
         Lazy<ISetRepository> Set { get; set; }
         Lazy<IUserCartRepository> UserCart { get; set; }
         Lazy<INewsRepository> News { get; set; }

    }
}
