using RTL.TVMaze.Application.Contract.Models;
using System.Threading.Tasks;

namespace RTL.TVMaze.Application.Dependencies.Contract
{
    public interface IDataStorage
    {
        Task<Show[]> GetShowsAsync(int take, int skip);
    }
}
