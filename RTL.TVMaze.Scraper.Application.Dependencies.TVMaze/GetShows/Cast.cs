using Newtonsoft.Json;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze.GetShows
{
    internal class Cast
    {
        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}
