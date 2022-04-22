using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Domains.Common
{
    public class AppConfiguration
    {
        private readonly string _systemLink = string.Empty;
        private readonly string _connectionString = string.Empty;
        private readonly string _Issuer = string.Empty;
        private readonly string _Audience = string.Empty;
        private readonly string _JWTKey = string.Empty;
        private readonly string _AccountSid = string.Empty;
        private readonly string _AuthToken = string.Empty;
        private readonly string _MobileSMSNumber = string.Empty;

        public AppConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            IConfigurationRoot root = configurationBuilder.Build();
            _Issuer = root.GetSection("Jwt").GetSection("Issuer").Value;
            _Audience = root.GetSection("Jwt").GetSection("Audience").Value;
            _JWTKey = root.GetSection("Jwt").GetSection("Key").Value;
            _connectionString = root.GetConnectionString("SqlConnection");
            _AccountSid = root.GetSection("OTP").GetSection("AccountSid").Value;
            _AuthToken = root.GetSection("OTP").GetSection("AuthToken").Value;
            _MobileSMSNumber = root.GetSection("OTP").GetSection("MobileNUmber").Value;

        }
        public string ConnectionString
        {
            get => _connectionString;
        }

        public string AccountSid
        {
            get => _AccountSid;
        }
        public string AuthToken
        {
            get => _AuthToken;
        }
        public string MobileNumber
        {
            get => _MobileSMSNumber;
        }
        public string Issuer
        {
            get => _Issuer;
        }

        public string Audience
        {
            get => _Audience;
        }
        public string JWTKey
        {
            get => _JWTKey;
        }

    }
}
