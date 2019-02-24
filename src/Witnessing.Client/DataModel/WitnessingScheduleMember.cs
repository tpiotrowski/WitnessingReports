using System;
using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class WitnessingScheduleMember
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("witnessing_id", Required = Required.Always)]
        public long WitnessingId { get; set; }

        [JsonProperty("user_id", Required = Required.Always)]
        public long UserId { get; set; }

        [JsonProperty("user_first_name", Required = Required.Always)]
        public string UserFirstName { get; set; }

        [JsonProperty("user_last_name", Required = Required.Always)]
        public string UserLastName { get; set; }

        [JsonProperty("user_phone", Required = Required.AllowNull)]
        public object UserPhone { get; set; }

        [JsonProperty("hour_id", Required = Required.Always)]
        public long HourId { get; set; }

        [JsonProperty("location_id", Required = Required.Always)]
        public long LocationId { get; set; }

        [JsonProperty("member_key", Required = Required.Always)]
        public long MemberKey { get; set; }

        [JsonProperty("date", Required = Required.Always)]
        public DateTimeOffset Date { get; set; }
    }
}