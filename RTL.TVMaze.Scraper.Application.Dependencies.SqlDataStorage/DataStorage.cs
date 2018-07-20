using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RTL.TVMaze.Scraper.Application.Dependencies.Contract;
using System;
using System.Threading.Tasks;

namespace RTL.TVMaze.Scraper.Application.Dependencies.SqlDataStorage
{
    public class DataStorage : IDataStorage
    {
        private TVMazeContextFactory _contextFactory;

        public DataStorage(TVMazeContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<int?> GetLastUpdateTimeAsync()
        {
            using (var context = _contextFactory.Build())
            {
                return await context.Shows.AnyAsync()
                    ? await context.Shows.MaxAsync(x => x.LastUpdateTime)
                    : (int?)null;
            }
        }

        // TODO: use bulk insert
        public async Task InsertOrUpdateAsync(params Application.Contract.Models.ShowWithLastUpdatedTime[] shows)
        {
            using (var context = _contextFactory.Build())
            {
                foreach (var showWithLastUpdateTime in shows)
                {
                    var existingShow = await context.Shows.SingleOrDefaultAsync(x => x.Id == showWithLastUpdateTime.Show.Id);

                    var newestShow = new Show
                    {
                        Id = showWithLastUpdateTime.Show.Id,
                        LastUpdateTime = showWithLastUpdateTime.LastUpdateTime,
                        Model = JsonConvert.SerializeObject(showWithLastUpdateTime.Show)
                    };

                    if (existingShow != null)
                    {
                        existingShow.LastUpdateTime = newestShow.LastUpdateTime;
                        existingShow.Model = newestShow.Model;
                    }
                    else
                    {
                        context.Shows.Add(newestShow);
                    }
                    
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
