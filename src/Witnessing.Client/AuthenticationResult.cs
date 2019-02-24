using Witnessing.Client.DataModel;

namespace Witnessing.Client
{
    public partial class AuthenticationResult
    {
        public string AccessToken { get; set; }
        public string  Uid { get; set; }
        public string Client { get; set; }
        public AuthenticationUser User { get; set; }
    }
}