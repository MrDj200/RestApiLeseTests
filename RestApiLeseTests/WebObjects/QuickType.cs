// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using MarketAPI;
//
//    var baseItems = BaseItems.FromJson(jsonString);

namespace MarketAPI
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class BaseItems
    {
        [JsonProperty("payload")]
        public Payload Payload { get; set; }
    }

    public partial class Payload
    {
        [JsonProperty("items")]
        public Items Items { get; set; }
    }

    public partial class Items
    {
        [JsonProperty("en")]
        public List<En> En { get; set; }
    }

    public partial class En
    {
        [JsonProperty("item_name")]
        public string ItemName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url_name")]
        public string UrlName { get; set; }
    }

    public partial class BaseItems
    {
        public static BaseItems FromJson(string json) => JsonConvert.DeserializeObject<BaseItems>(json, MarketAPI.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this BaseItems self) => JsonConvert.SerializeObject(self, MarketAPI.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
