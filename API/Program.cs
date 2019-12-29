using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // check the creation of the database 
            var host = CreateWebHostBuilder(args).Build();
            // get reference to data context 
            using (var scope = host.Services.CreateScope())
            {
                // get refernce to services 
                var services = scope.ServiceProvider; 
                try 
                {
                    var context = services.GetRequiredService<DataContext>(); 
                    context.Database.Migrate(); // create database if not already exists 
                    Seed.SeedData(context);
                }
                catch (Exception ex) 
                {
                    var logger = services.GetRequiredService<ILogger<Program>>(); 
                    logger.LogError(ex, "An Error occured during migration");
                }
            }

            // runs our application
            host.Run(); 
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
