using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class Locations
    {
        [JsonProperty("witnessing_locations", Required = Required.Always)]
        public WitnessingLocation[] WitnessingLocations { get; set; }

        public static Locations FromJson(string json) => JsonConvert.DeserializeObject<Locations>(json, Witnessing.Client.DataModel.Converter.Settings);
    }
}