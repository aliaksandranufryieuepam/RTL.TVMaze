using AutoFixture;
using NSubstitute;
using RTL.TVMaze.Scraper.Application.Contract.Models;
using RTL.TVMaze.Scraper.Application.Dependencies.Contract;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RTL.TVMaze.Scrapper.Application.Tests
{
    public class ScraperTests
    {
        private Fixture _fixture;

        public ScraperTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task RunAsync_FirstRun_InsertsAllUpdates()
        {
            var client = Substitute.For<ITVMazeClient>();
            var storage = Substitute.For<IDataStorage>();

            var scraper = new RTL.TVMaze.Scraper.Application.Scraper(client, storage);

            var updates = _fixture.CreateMany<ShowUpdate>().ToArray();

            client.GetShowUpdatesAsync()
                .Returns(Task.FromResult(updates));
            
            // Act
            await scraper.RunAsync();

            // Assert
            await storage.Received(updates.Length).InsertOrUpdateAsync(Arg.Any<ShowWithLastUpdatedTime>());
        }
    }
}
