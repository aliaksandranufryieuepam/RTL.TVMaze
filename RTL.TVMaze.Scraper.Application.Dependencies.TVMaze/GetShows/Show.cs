using Newtonsoft.Json;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze.GetShows
{
    internal class Show
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded {get;set; }
    }
}
