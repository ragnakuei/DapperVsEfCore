using Microsoft.Extensions.Configuration;

namespace ConsoleApp1
{
    public class ConfigReader
    {
        private static IConfigurationRoot _config;

        static ConfigReader()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                .Build();
        }

        public static string GetConnectionString(string name)
        {
            return _config[$"ConnectionStrings:{name}"];
        }
    }
}