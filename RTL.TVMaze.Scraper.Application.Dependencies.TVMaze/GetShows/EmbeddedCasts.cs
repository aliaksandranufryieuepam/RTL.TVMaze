using Newtonsoft.Json;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze.GetShows
{
    internal class Embedded
    {
        [JsonProperty("cast")]
        public Cast[] Casts { get; set; }
    }
}
