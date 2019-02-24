using System;
using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class WitnessingMember
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("email", Required = Required.Always)]
        public string Email { get; set; }

        [JsonProperty("created_at", Required = Required.Always)]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("first_name", Required = Required.Always)]
        public string FirstName { get; set; }

        [JsonProperty("last_name", Required = Required.Always)]
        public string LastName { get; set; }

        [JsonProperty("phone", Required = Required.Always)]
        public string Phone { get; set; }

        [JsonProperty("confirmed_at", Required = Required.Always)]
        public DateTimeOffset ConfirmedAt { get; set; }

        [JsonProperty("congregation_id", Required = Required.AllowNull)]
        public long? CongregationId { get; set; }

        [JsonProperty("congregation_name", Required = Required.AllowNull)]
        public string CongregationName { get; set; }
    }
}