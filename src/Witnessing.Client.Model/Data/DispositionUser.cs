using System;
using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class DispositionUser
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
        //[JsonConverter(typeof(ParseStringConverter))]
        public string Phone { get; set; }

        [JsonProperty("confirmed_at", Required = Required.Always)]
        public DateTimeOffset ConfirmedAt { get; set; }

        [JsonProperty("type_id", Required = Required.Always)]
        public long TypeId { get; set; }

        [JsonProperty("shifts_count", Required = Required.Always)]
        public long ShiftsCount { get; set; }

        [JsonProperty("dispositions_count", Required = Required.Always)]
        public long DispositionsCount { get; set; }

        [JsonProperty("has_privileges", Required = Required.Always)]
        public bool HasPrivileges { get; set; }

        [JsonIgnore]
        public long HourId { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }

    }
}