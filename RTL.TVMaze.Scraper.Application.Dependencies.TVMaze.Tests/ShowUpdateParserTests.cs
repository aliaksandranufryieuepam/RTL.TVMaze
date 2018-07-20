using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze.Tests
{
    public class ShowUpdateParserTests
    {
        [Fact]
        public void Parse_OneUpdate()
        {
            var parser = new ShowUpdatesParser();
            var firstJanuary2018 = 1514764800;

            var updates = parser.Parse($"{{\"1\":{firstJanuary2018}}}");

            updates.Should().NotBeNull();
            updates.Length.Should().Be(1);
            updates.Single().Id.Should().Be(1);
            updates.Single().LastUpdateTime.Should().Be(firstJanuary2018);
        }

        [Fact]
        public void Parse_TwoUpdates()
        {
            var parser = new ShowUpdatesParser();

            var updates = parser.Parse("{\"1\":1529612668,\"2\":1529283091,\"3\":1515870544");

            updates.Should().NotBeNull();
        }
    }
}
