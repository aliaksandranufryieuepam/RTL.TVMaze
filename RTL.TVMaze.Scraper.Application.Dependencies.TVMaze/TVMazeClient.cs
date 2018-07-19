using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RTL.TVMaze.Scraper.Application.Contract.Models;
using RTL.TVMaze.Scraper.Application.Dependencies.Contract;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze
{
    public class TVMazeClient : ITVMazeClient
    {
        private const string host = "http://api.tvmaze.com";
        private const string updatesUrl = "/updates/shows";
        private const string showsUrl = "/shows/1?embed=cast";

        private HttpClient _httpClient;

        public TVMazeClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ShowUpdate[]> GetShowUpdatesAsync()
        {
            var response = await _httpClient.GetAsync($"{host}{updatesUrl}");

            var json = await response.Content.ReadAsStringAsync();

            var updates = new ShowUpdatesParser().Parse(json);

            return updates;
        }

        public async Task<Show> GetShowAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{host}{showsUrl}");

            var json = await response.Content.ReadAsStringAsync();

            var show = JsonConvert.DeserializeObject<GetShows.Show>(json);

            //TODO: I'll use Automapper if the mapping gets more complicated
            return new Show
            {
                Id = show.Id,
                Name = show.Name,
                Casts = show.Embedded.Casts.Select(x => new Cast
                {
                    Id = x.Person.Id,
                    Name = x.Person.Name,
                    Birthday = x.Person.Birthday
                })
                .ToArray()
            };
        }
    }
}
