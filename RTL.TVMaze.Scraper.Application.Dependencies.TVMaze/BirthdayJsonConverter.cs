using Newtonsoft.Json;
using System;
using System.Linq;

namespace RTL.TVMaze.Scraper.Application.Dependencies.TVMaze
{
    public class BirthdayJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string text = reader.Value?.ToString();

            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var dateParts = text.Split('-').Select(x => int.Parse(x)).ToArray();

            // incorrect date
            if (dateParts.Any(x => x == 0))
            {
                return null;
            }

            return new DateTime(dateParts[0], dateParts[1], dateParts[2]);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
