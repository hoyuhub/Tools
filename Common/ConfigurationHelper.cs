using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Common
{
    public class ConfigurationHelper
    {
        public ConfigurationHelper()
        {
            IHostingEnvironment env = MyServiceProvider.ServiceProvider.GetRequiredService<IHostingEnvironment>();
            config = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
        public IConfiguration config { get; set; }
        public T GetAppSettings<T>(string key) where T : class, new()
        {
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }



    }
}
