using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class Hours
    {
        [JsonProperty("witnessing_hours", Required = Required.Always)]
        public WitnessingHour[] WitnessingHours { get; set; }

        public static Hours FromJson(string json) => JsonConvert.DeserializeObject<Hours>(json, Witnessing.Client.DataModel.Converter.Settings);
    }






}