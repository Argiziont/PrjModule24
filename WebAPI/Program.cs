using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                    builder.UseUrls("http://localhost:8443/");
                });
    }
}