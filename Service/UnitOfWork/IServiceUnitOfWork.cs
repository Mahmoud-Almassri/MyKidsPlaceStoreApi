
using Service.Interfaces;
using Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.UnitOfWork
{
    public interface IServiceUnitOfWork : IDisposable
    {
        Lazy<IApplicationExceptionsService> ApplicationExceptions { get; set; }
        Lazy<IAuthService> Auth { get; set; }
        Lazy<IBrandService> Brand { get; set; }
        Lazy<ICartItemService> CartItem { get; set; }
        Lazy<ICategoryService> Category { get; set; }
        Lazy<IItemService> Item { get; set; }
        Lazy<IItemImagesService> ItemImages { get; set; }
        Lazy<IOrderService> Order { get; set; }
        Lazy<ISaleService> Sale { get; set; }
        Lazy<ISetService> Set { get; set; }
        Lazy<ISubCategoryService> SubCategory { get; set; }
        Lazy<IUserCartService> UserCart { get; set; }
        Lazy<INewsService> News { get; set; }


    }
}
