using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace SamaDelivery.Api.Helpers
{
    public static class ServiceCollectionExtensions
    {

        public static void AddFirebase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment contentRootPath)

        {

            var filePath = Path.Combine(contentRootPath.ContentRootPath, "mykidsplacestore-firebase-adminsdk-norir-2790744bc2.json");

            FirebaseApp.Create(new AppOptions()

            {

                Credential = GoogleCredential.FromFile(filePath),

            });

        }
    }
}
