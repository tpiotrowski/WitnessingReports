using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public partial class AuthenticationResultUser
    {
        [JsonProperty("data", Required = Required.Always)]
        public AuthenticationUser User { get; set; }
    }
}