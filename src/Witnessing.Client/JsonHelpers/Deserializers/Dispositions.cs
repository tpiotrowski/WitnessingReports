using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class Dispositions
    {
        [JsonProperty("users", Required = Required.Always)]
        public DispositionUser[] Users { get; set; }
        public static Dispositions FromJson(string json) => JsonConvert.DeserializeObject<Dispositions>(json, Witnessing.Client.DataModel.Converter.Settings);
    }
}