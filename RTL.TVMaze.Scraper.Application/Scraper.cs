using System;
using System.Linq;
using System.Threading.Tasks;
using RTL.TVMaze.Scraper.Application.Contract;
using RTL.TVMaze.Scraper.Application.Contract.Models;
using RTL.TVMaze.Scraper.Application.Dependencies.Contract;

namespace RTL.TVMaze.Scraper.Application
{
    public class Scraper : IScraper
    {
        private ITVMazeClient _client;
        private IDataStorage _dataStorage;

        public Scraper(ITVMazeClient client, IDataStorage dataStorage)
        {
            _client = client;
            _dataStorage = dataStorage;
        }

        public async Task RunAsync()
        {
            ShowUpdate[] updates = await _client.GetShowUpdatesAsync();

            DateTime? lastUpdateTime = await _dataStorage.GetLastUpdateTimeAsync();

            Func<ShowUpdate, bool> predicate;
            
            if (lastUpdateTime.HasValue)
            {
                predicate = x => x.LastUpdateTime > lastUpdateTime;
            }
            else
            {
                predicate = x => true;
            }

            var outdatedShows = updates
                .Where(predicate)
                .OrderBy(x => x.LastUpdateTime)
                .Select(x => x.Id)
                .ToArray();

            foreach (var show in outdatedShows)
            {
                await UpdateShowAsync(show);
            }
        }

        private async Task UpdateShowAsync(int id, int retryCount = 3)
        {
            try
            {
                var show = await _client.GetShowAsync(id);
                await _dataStorage.InsertOrUpdateAsync(show);
            }
            catch (Exception)
            {
                if (retryCount > 0)
                {
                    await UpdateShowAsync(id, retryCount - 1);
                }

                // TODO: send a message to a queue to retry to update the show later or just log the fail

                throw;
            }            
        }
    }
}
