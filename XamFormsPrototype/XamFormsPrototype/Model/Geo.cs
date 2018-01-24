using Newtonsoft.Json;

namespace XamFormsPrototype.Model
{
    public class Geo
    {
        [JsonProperty("lat")]
        public string Lat { get; set; }
        [JsonProperty("lng")]
        public string Lng { get; set; }
    }
}
