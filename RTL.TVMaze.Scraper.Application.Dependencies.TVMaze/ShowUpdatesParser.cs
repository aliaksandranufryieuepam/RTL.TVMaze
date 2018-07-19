using System.Linq;
using RTL.TVMaze.Scraper.Application.Contract.Models;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze
{
    public class ShowUpdatesParser
    {
        public ShowUpdate[] Parse(string json)
        {
            var updates = json.Trim('{', '}')
                .Split(',')
                .Select(x =>
                {
                    var item = x.Split(':').ToArray();

                    var id = int.Parse(item[0].Trim('"'));
                    var unixTimeStamp = int.Parse(item[1]);

                    return new ShowUpdate
                    {
                        Id = id,
                        LastUpdateTime = unixTimeStamp.ToDateTime()
                    };
                });

            return updates.ToArray();
        }
    }
}
