namespace BusinessOwnerRecord
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class BusinessOwner
    {
        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("doingBusinessAsName")]
        public string DoingBusinessAsName { get; set; }

        [JsonProperty("ownerFirstName")]
        public string OwnerFirstName { get; set; }

        [JsonProperty("ownerLastName")]
        public string OwnerLastName { get; set; }

        [JsonProperty("ownerTitle")]
        public string OwnerTitle { get; set; }

        [JsonProperty("licenseNumber")]
        public string LicenseNumber { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("businessActivity")]
        public string BusinessActivity { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }

    public partial class BusinessOwner
    {
        public static BusinessOwner[] FromJson(string json) => JsonConvert.DeserializeObject<BusinessOwner[]>(json, BusinessOwnerRecord.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this BusinessOwner[] self) => JsonConvert.SerializeObject(self, BusinessOwnerRecord.Converter.Settings);
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
}
