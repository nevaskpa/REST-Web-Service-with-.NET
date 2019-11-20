namespace OwnerRecord
{
    using System;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Owner
    {
        [JsonProperty("account_number")]        
        public string AccountNumber { get; set; }

        [JsonProperty("doing_business_as_name")]
        public string DoingBusinessAsName { get; set; }

        [JsonProperty("owner_first_name")]
        public string OwnerFirstName { get; set; }

        [JsonProperty("owner_middle_initial", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerMiddleInitial { get; set; }

        [JsonProperty("owner_last_name")]
        public string OwnerLastName { get; set; }

        [JsonProperty("owner_title")]
        public string OwnerTitle { get; set; }
    }

    public partial class Owner
    {
        public static Owner[] FromJson(string json) => JsonConvert.DeserializeObject<Owner[]>(json, OwnerRecord.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Owner[] self) => JsonConvert.SerializeObject(self, OwnerRecord.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
