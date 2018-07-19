using System.Threading.Tasks;
using RTL.TVMaze.Scraper.Application.Contract.Models;

namespace RTL.TVMaze.Scraper.Application.Dependencies.Contract
{
    public interface ITVMazeClient
    {
        Task<ShowUpdate[]> GetShowUpdatesAsync();

        Task<Show> GetShowAsync(int id);
    }
}
