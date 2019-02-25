using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class WitnessingHour
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("day_id", Required = Required.Always)]
        public long DayId { get; set; }

        [JsonProperty("witnessing_id", Required = Required.Always)]
        public long WitnessingId { get; set; }

        [JsonProperty("start", Required = Required.Always)]
        public string Start { get; set; }

        [JsonProperty("end", Required = Required.Always)]
        public string End { get; set; }
    }
}