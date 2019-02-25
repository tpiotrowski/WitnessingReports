using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class WitnessingLocation
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("witnessing_id", Required = Required.Always)]
        public long WitnessingId { get; set; }
    }
}