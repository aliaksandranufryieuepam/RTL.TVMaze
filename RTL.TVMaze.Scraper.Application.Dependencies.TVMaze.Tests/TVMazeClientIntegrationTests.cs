using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze.Tests
{
    public class TVMazeClientIntegrationTests
    {
        private static HttpClient HttpClient;

        static TVMazeClientIntegrationTests()
        {
            HttpClient = new HttpClient();
        }

        private TVMazeClient _tVMazeClient;

        public TVMazeClientIntegrationTests()
        {
            _tVMazeClient = new TVMazeClient(HttpClient);
        }

        [Fact]
        public async Task GetShowUpdates()
        {
            var shows = await _tVMazeClient.GetShowUpdatesAsync();

            shows.Should().NotBeNull();
            shows.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetShowAsync_GetFirst()
        {
            var updates = await _tVMazeClient.GetShowUpdatesAsync();
            var showId = updates[0].Id;
            var show = await _tVMazeClient.GetShowAsync(showId);

            show.Should().NotBeNull();

            show.Id.Should().Be(showId);

            show.Name.Should().NotBeNull();
            show.Name.Should().NotBeEmpty();

            show.Casts.Should().NotBeNull();
            show.Casts.Should().NotBeEmpty();
        }
    }
}
