using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class Schedule
    {
        [JsonProperty("witnessing_schedule_members", Required = Required.Always)]
        public WitnessingScheduleMember[] WitnessingScheduleMembers { get; set; }

        public static Schedule FromJson(string json) => JsonConvert.DeserializeObject<Schedule>(json, Witnessing.Client.DataModel.Converter.Settings);
    }
}