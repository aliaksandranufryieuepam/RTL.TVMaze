using Microsoft.EntityFrameworkCore;

namespace RTL.TVMaze.Scraper.Application.Dependencies.SqlDataStorage
{
    public class TVMazeContextFactory
    {
        private string _connectionString;

        public TVMazeContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public TVMazeContext Build()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TVMazeContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            return new TVMazeContext(optionsBuilder.Options);
        }
    }
}
