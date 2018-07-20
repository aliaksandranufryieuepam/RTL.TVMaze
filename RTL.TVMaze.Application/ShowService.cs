using System.Threading.Tasks;
using RTL.TVMaze.Application.Contract;
using RTL.TVMaze.Application.Contract.Models;
using RTL.TVMaze.Application.Dependencies.Contract;

namespace RTL.TVMaze.Application
{
    public class ShowService : IShowService
    {
        private IDataStorage _dataStorage;

        public ShowService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task<Show[]> GetShowsAsync(int take, int skip = 0)
        {
            var shows = await _dataStorage.GetShowsAsync(take, skip);

            // TODO: Order casts If you are not sure that they were saved ordered by birthday

            return shows;
        }
    }
}
