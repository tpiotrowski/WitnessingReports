using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class AuthenticationUser
    {
        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("email", Required = Required.Always)]
        public string Email { get; set; }

        [JsonProperty("provider", Required = Required.Always)]
        public string Provider { get; set; }

        [JsonProperty("first_name", Required = Required.Always)]
        public string FirstName { get; set; }

        [JsonProperty("last_name", Required = Required.Always)]
        public string LastName { get; set; }

        [JsonProperty("phone", Required = Required.Always)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Phone { get; set; }

        [JsonProperty("gender", Required = Required.AllowNull)]
        public object Gender { get; set; }

        [JsonProperty("uid", Required = Required.Always)]
        public string Uid { get; set; }

        [JsonProperty("description", Required = Required.AllowNull)]
        public object Description { get; set; }

        [JsonProperty("deleted", Required = Required.Always)]
        public bool Deleted { get; set; }
    }
}