using RTL.TVMaze.Application.Contract.Models;
using System.Threading.Tasks;

namespace RTL.TVMaze.Application.Contract
{
    public interface IShowService
    {
        Task<Show[]> GetShowsAsync(int take, int skip = 0);
    }
}
