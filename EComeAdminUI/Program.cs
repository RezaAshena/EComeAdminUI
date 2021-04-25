using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EComeAdminUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetEbConfig();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetEbConfig()
        {
            ConfigurationBuilder tempConfigBuilder = new ConfigurationBuilder();
            var filePath = @"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration";
            if (File.Exists(filePath))
            {
                Console.WriteLine($"The file at - [{filePath}] does exists!");
            }
            else
            {
                Console.WriteLine($"The file at - [{filePath}] does not exists!");
            }

            tempConfigBuilder.AddJsonFile(
                filePath,
                optional: true,
                reloadOnChange: true
            );
            IConfigurationRoot configuration = tempConfigBuilder.Build();
            Dictionary<string, string> ebEnv =
                configuration.GetSection("iis:env")
                    .GetChildren()
                    .Select(pair => pair.Value.Split(new[] { '=' }, 2))
                    .ToDictionary(keypair => keypair[0], keypair => keypair[1]);

            foreach (KeyValuePair<string, string> keyVal in ebEnv)
            {
                Environment.SetEnvironmentVariable(keyVal.Key, keyVal.Value);
            }
        }
    }
}
