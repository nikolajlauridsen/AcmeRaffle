using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeRaffle.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AcmeRaffle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            CreateDbIfNotExist(host);
            host.Run();
        }

        private static void CreateDbIfNotExist(IHost host)
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                RaffleDbContext context = scope.ServiceProvider.GetRequiredService<RaffleDbContext>();
                DbInitializer.InitializeRaffle(context);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
