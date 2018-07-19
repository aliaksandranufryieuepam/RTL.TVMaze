using System;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this int unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(unixTimeStamp);
        }
    }
}
