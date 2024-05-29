using Microsoft.Identity.Client;
using OpenIdAuthServer.Models;
using System.Text.Json;

namespace OpenIdAuthServer.Helper
{
    public class ConfigReader
    {
        private readonly AppConfig _config;

        public ConfigReader(string filePath)
        {
            _config = LoadConfiguration(filePath);
        }

        private AppConfig LoadConfiguration(string filePath)
        {
            try
            {
                var jsonString = File.ReadAllText(filePath);
                var config = JsonSerializer.Deserialize<AppConfig>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (config == null)
                    throw new Exception("Configuration could not be loaded.");

                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or parsing configuration file: {ex.Message}");
                throw;
            }
        }

        public string GetJwtKey()
        {
            return _config.Jwt.Key;
        }

        public string GetJwtIssuer()
        {
            return _config.Jwt.Issuer;
        }

        public string GetJwtAudience()
        {
            return _config.Jwt.Audience;
        }
    }

    public class JwtConfig
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

    public class AppConfig
    {
        public JwtConfig Jwt { get; set; }
    }

}
