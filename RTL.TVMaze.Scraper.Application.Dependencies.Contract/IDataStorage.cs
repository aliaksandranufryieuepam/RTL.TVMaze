using System;
using System.Threading.Tasks;
using RTL.TVMaze.Scraper.Application.Contract.Models;

namespace RTL.TVMaze.Scraper.Application.Dependencies.Contract
{
    public interface IDataStorage
    {
        Task<DateTime?> GetLastUpdateTimeAsync();
        Task InsertOrUpdateAsync(params Show[] shows);
    }
}
