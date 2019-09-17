using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DatingApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            //uzyskanie instacji konteksu danuch i przekazac do metody seed
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>(); //kontekst bazy na ktorym wykonujemy wszystko
                    context.Database.Migrate(); //tworzy baze gdy jej nie ma i wykonuje oczekujace migracje
                    Seed.SeedUsers(context); //przekazujemy kontekst, zeby moc na nim robi CRUDy w bazie w metodzie SeedUsers
                                             //ogolnie wywolanie metody powoduje dodanie pierwszych userow do bazy jezeli nie ma zadnego   
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured during migrations");
                    //Console.WriteLine(e.ToString()) 
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
