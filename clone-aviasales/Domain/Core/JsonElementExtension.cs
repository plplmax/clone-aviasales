using clone_aviasales.Domain.Model;
using System;
using System.Text.Json;

namespace clone_aviasales.Domain.Core
{
    public static class JsonElementExtension
    {
        private const string TRANSLATIONS_KEY = "name_translations";
        private const string TIMEZONE_KEY = "time_zone";
        private const string IATA_CODE_KEY = "code";
        private const string LOCALE_RU = "ru";
        private const string LOCALE_EN = "en";

        public static City ToCity(this JsonElement city)
        {
            return new City()
            {
                Name = city.GetProperty(TRANSLATIONS_KEY).GetProperty(LOCALE_RU).GetString(),
                Timezone = city.GetProperty(TIMEZONE_KEY).GetString()
            };
        }

        public static Airline ToAirline(this JsonElement airline)
        {
            return new Airline()
            {
                Name = airline.GetProperty(TRANSLATIONS_KEY).GetProperty(LOCALE_EN).GetString()
            };
        }

        public static string GetIataCode(this JsonElement element) => element.GetProperty(IATA_CODE_KEY).GetString();
    }
}
