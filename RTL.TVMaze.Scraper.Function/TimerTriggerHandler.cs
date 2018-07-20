using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using RTL.TVMaze.Scraper.Application.Dependencies.SqlDataStorage;
using RTL.TVMaze.Scraper.Application.Dependencies.TVMaze;

namespace RTL.TVMaze.Scraper.Function
{
    public static class TimerTriggerHandler
    {
        private static HttpClient HttpClient;

        static TimerTriggerHandler()
        {
            HttpClient = new HttpClient();
        }

        [FunctionName("TimerTriggerHandler")]
        public static async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            var client = new TVMazeClient(HttpClient);

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:TVMazeDatabase", EnvironmentVariableTarget.Process);

            var contextFactory = new TVMazeContextFactory(connectionString);
            var dataStorage = new DataStorage(contextFactory);

            var scraper = new Application.Scraper(client, dataStorage);

            await scraper.RunAsync();
        }
    }
}
