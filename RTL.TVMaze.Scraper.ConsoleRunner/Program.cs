using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTL.TVMaze.Scraper.Application.Contract;
using RTL.TVMaze.Scraper.Application.Dependencies.Contract;
using RTL.TVMaze.Scraper.Application.Dependencies.SqlDataStorage;
using RTL.TVMaze.Scraper.Application.Dependencies.TVMaze;
using System;
using System.Net.Http;

namespace RTL.TVMaze.Scraper.ConsoleRunner
{
    class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<HttpClient>();
                    services.AddTransient<ITVMazeClient, TVMazeClient>();

                    var connectionString = hostContext.Configuration.GetConnectionString("TVMazeDatabase");
                    services.AddSingleton(serviceProvider => new TVMazeContextFactory(connectionString));
                    services.AddTransient<IDataStorage, DataStorage>();
                    services.AddTransient<IScraper, Scraper.Application.Scraper>();

                    services.AddHostedService<ScrapTVMazeEvery5MinutesService>();
                })
                .Build();

            host.Run();
        }
    }
}
