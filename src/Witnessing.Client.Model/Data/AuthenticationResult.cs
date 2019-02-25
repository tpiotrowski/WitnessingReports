using Witnessing.Client.DataModel;

namespace Witnessing.Client
{

    public class AuthData
    {
        public string AccessToken { get; set; }
        public string Uid { get; set; }
        public string Client { get; set; }
    }

    public partial class AuthenticationResult : AuthData
    {
        
        public AuthenticationUser User { get; set; }
    }
}