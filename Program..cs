using Microsoft.AspNetCore;
using Simple_Api.Helpers;

namespace Simple_Api
{
    public class Program
    {
        #region -= Properties =-
        public static IConfiguration Configuration { get; set; }
        #endregion
        
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(Configuration.GetSection("HostingURL").Value);
    }
}