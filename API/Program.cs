using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        //dotnet run goes to the main method and run the calls inside it
        //Microsoft.AspNetCore.Server.Kestrel is the server which hosts the web API
        public static void Main(string[] args)
        {
            //create the Kestrel server with some default settings and then run it
            // CreateHostBuilder(args).Build().Run();

            //To cater the seeding products
            var host = CreateHostBuilder(args).Build();
            //using automatically Dispose scope when this is no longer useful
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
            //log any errors if any, when we do this
            //ILogger<Program> the class we run the logger
            //catch errors before the developer exception page comes in to the scene, bcz it comes later
            //we want to get any error before it loads developer exception page.
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                //it is like ||dotnet ef database update||
                context.Database.Migrate();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Problem in migrating Data");
            }
            //for release the resources used for the Services
            // finally
            // {
            //     //Dispose the scope and any resources using
            //     scope.Dispose();
            // }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //use thee startup class for additional configuration and services
                    webBuilder.UseStartup<Startup>();
                });
    }
}
