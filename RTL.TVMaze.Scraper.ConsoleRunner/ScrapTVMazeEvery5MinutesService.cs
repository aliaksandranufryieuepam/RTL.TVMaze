using Microsoft.Extensions.Hosting;
using RTL.TVMaze.Scraper.Application.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RTL.TVMaze.Scraper.ConsoleRunner
{
    internal class ScrapTVMazeEvery5MinutesService : IHostedService, IDisposable
    {
        private Timer _timer;
        private IScraper _scraper;

        public ScrapTVMazeEvery5MinutesService(IScraper scraper)
        {
            _scraper = scraper;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Scrap, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void Scrap(object state)
        {
            _scraper.RunAsync().Wait();            
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
