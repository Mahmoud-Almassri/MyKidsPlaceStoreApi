using Domains.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repository;
using Repository.Context;
using Repository.UnitOfWork;
using Service.Interfaces;
using Service.Interfaces.Common;
using Service.Services;
using Service.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.UnitOfWork
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;
        private readonly LoggedInUserService _loggedInUserService;
        private readonly PushNotificationService pushNotificationService;
        public Lazy<IApplicationExceptionsService> ApplicationExceptions { get; set; }
        public Lazy<IAuthService> Auth { get; set; }
        public Lazy<IBrandService> Brand { get; set; }
        public Lazy<ICartItemService> CartItem { get; set; }
        public Lazy<ICategoryService> Category { get; set; }
        public Lazy<IItemImagesService> ItemImages { get; set; }
        public Lazy<IOrderService> Order { get; set; }
        public Lazy<IItemService> Item { get; set; }
        public Lazy<ISaleService> Sale { get; set; }
        public Lazy<ISetService> Set { get; set; }
        public Lazy<ISubCategoryService> SubCategory { get; set; }
        public Lazy<IUserCartService> UserCart { get; set; }
        public Lazy<IUserService> User { get; set; }
        public Lazy<IPushNotificationService>  PushNotification { get; set; }
        public Lazy<INewsService>  News { get; set; }



        public ServiceUnitOfWork(
           MyKidsStoreDbContext context,
           UserManager<ApplicationUser> userManager,
           IHttpContextAccessor httpContextAccessor,
           SignInManager<ApplicationUser> signInManager
           )
        {
            _loggedInUserService = new LoggedInUserService(httpContextAccessor);
            pushNotificationService = new PushNotificationService(userManager);
            _repositoryUnitOfWork = new RepositoryUnitOfWork(context);
            ApplicationExceptions = new Lazy<IApplicationExceptionsService>(() => new ApplicationExceptionsService(_repositoryUnitOfWork));

            Auth = new Lazy<IAuthService>(() => new AuthService(userManager, _repositoryUnitOfWork, signInManager, _loggedInUserService));
            Brand = new Lazy<IBrandService>(() => new BrandService(_repositoryUnitOfWork));
            CartItem = new Lazy<ICartItemService>(() => new CartItemService(_repositoryUnitOfWork, _loggedInUserService));
            Category = new Lazy<ICategoryService>(() => new CategoryService(_repositoryUnitOfWork));
            Item = new Lazy<IItemService>(() => new ItemService(_repositoryUnitOfWork, context));
            ItemImages = new Lazy<IItemImagesService>(() => new ItemImageservice(_repositoryUnitOfWork, context));
            Order = new Lazy<IOrderService>(() => new OrderService(_repositoryUnitOfWork, pushNotificationService, context, _loggedInUserService));
            Sale = new Lazy<ISaleService>(() => new SaleService(_repositoryUnitOfWork));
            Set = new Lazy<ISetService>(() => new SetService(_repositoryUnitOfWork));
            UserCart = new Lazy<IUserCartService>(() => new UserCartService(_repositoryUnitOfWork));
            SubCategory = new Lazy<ISubCategoryService>(() => new SubCategoryService(_repositoryUnitOfWork));
            User = new Lazy<IUserService>(() => new UserService(userManager));
            News = new Lazy<INewsService>(() => new NewsService(context));
  
        }

        
        public void Dispose() {}
    }
}
