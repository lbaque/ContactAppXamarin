using Infrastructure.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Infrastructure.Helpers
{
    public static class Extenders
    {
        public static string Concatenar(params string[] texts)
        {
            var builder = new StringBuilder();
            foreach (var text in texts)
                builder.Append(text);
            return builder.ToString();
        }

        public static string Serializar(this IEntity entry, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(entry, settings);
        }

        public static IEntry Deserialize<IEntry>(this string json)
        {
            return JsonConvert.DeserializeObject<IEntry>(json);
        }

        public static double ToUnixTime(this DateTime dateTime)
        {
            return dateTime.Subtract(new DateTime(1900, 1, 1)).TotalSeconds;
        }
    }
}
