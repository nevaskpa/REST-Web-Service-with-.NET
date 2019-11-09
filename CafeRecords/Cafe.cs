namespace CafeRecord
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Cafe
    {
        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty(":@computed_region_43wa_7qmu")]
        public string ComputedRegion43Wa7Qmu { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("ward_precinct")]
        public string WardPrecinct { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("street_type")]
        public string StreetType { get; set; }

        [JsonProperty("payment_date")]
        public DateTimeOffset PaymentDate { get; set; }

        [JsonProperty(":@computed_region_rpca_8um6")]
        public string ComputedRegionRpca8Um6 { get; set; }

        [JsonProperty("issued_date")]
        public DateTimeOffset IssuedDate { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("precinct")]
        public string Precinct { get; set; }

        [JsonProperty("ward")]
        public string Ward { get; set; }

        [JsonProperty("doing_business_as_name")]
        public string DoingBusinessAsName { get; set; }

        [JsonProperty("police_district")]
        public string PoliceDistrict { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("street_direction")]
        public string StreetDirection { get; set; }

        [JsonProperty("permit_number")]
        public string PermitNumber { get; set; }

        [JsonProperty(":@computed_region_bdys_3d7i")]
        public string ComputedRegionBdys3D7I { get; set; }

        [JsonProperty(":@computed_region_6mkv_f3dw")]
        public string ComputedRegion6MkvF3Dw { get; set; }

        [JsonProperty("address_number_start")]
        public string AddressNumberStart { get; set; }

        [JsonProperty("address_number")]
        public string AddressNumber { get; set; }

        [JsonProperty("expiration_date")]
        public DateTimeOffset ExpirationDate { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("site_number")]
        public string SiteNumber { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("legal_name")]
        public string LegalName { get; set; }

        [JsonProperty(":@computed_region_vrxf_vc4k")]
        public string ComputedRegionVrxfVc4K { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("human_address")]
        public string HumanAddress { get; set; }

        [JsonProperty("needs_recoding")]
        public bool NeedsRecoding { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }

    public partial class Cafe
    {
        public static Cafe[] FromJson(string json) => JsonConvert.DeserializeObject<Cafe[]>(json, CafeRecord.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Cafe[] self) => JsonConvert.SerializeObject(self, CafeRecord.Converter.Settings);
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
