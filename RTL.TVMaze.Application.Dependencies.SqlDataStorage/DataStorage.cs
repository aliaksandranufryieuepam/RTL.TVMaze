using Dapper;
using Newtonsoft.Json;
using RTL.TVMaze.Application.Dependencies.Contract;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RTL.TVMaze.Application.Dependencies.SqlDataStorage
{
    public class DataStorage : IDataStorage
    {
        private string _connectionString;
        private const string ShowsTableName = "Shows";

        public DataStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Application.Contract.Models.Show[]> GetShowsAsync(int take, int skip)
        {
            var lastRow = skip + take;

            using (var connection = new SqlConnection(_connectionString))
            {
                var shows = await connection.QueryAsync<string>($@"
DECLARE  @StartRow INT
DECLARE  @EndRow INT

SET    @StartRow = @{nameof(skip)}
SET @EndRow = @{nameof(lastRow)}

SELECT {nameof(SqlDataStorage.Show.Model)}
FROM (
	SELECT    s.{nameof(SqlDataStorage.Show.Model)}, ROW_NUMBER() OVER(ORDER BY s.Id) AS RowNumber
    FROM    {ShowsTableName} s) s
WHERE    RowNumber > @StartRow
    AND RowNumber <= @EndRow", new { skip, lastRow });

                return shows.Select(x => JsonConvert.DeserializeObject<Application.Contract.Models.Show>(x)).ToArray();
            }
        }
    }
}
