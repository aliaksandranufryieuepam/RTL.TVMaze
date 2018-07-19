using RTL.TVMaze.Application.Contract.Models;
using System.Threading.Tasks;

namespace RTL.TVMaze.Application.Contract
{
    public interface ICastService
    {
        Task<Show[]> GetShows(int take, int skip = 0);
    }
}
