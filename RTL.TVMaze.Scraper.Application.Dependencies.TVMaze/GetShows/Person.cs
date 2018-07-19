using Newtonsoft.Json;
using System;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze.GetShows
{
    internal class Person
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }
    }
}
