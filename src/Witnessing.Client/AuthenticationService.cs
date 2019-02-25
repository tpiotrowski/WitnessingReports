using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Witnessing.Client.DataModel;
using Witnessing.Client.Model.Contract;

namespace Witnessing.Client
{
 

    public class AuthenticationService : RestServiceBase, IAuthenticationService
    {
        public AuthenticationService(HttpClient httpClient, ServiceConfiguration configuration) 
            : base(httpClient, configuration)
        {
        }

        public AuthenticationService(ServiceConfiguration configuration) 
            : base(configuration)
        {
        }

        public async Task<AuthenticationResult> LoginAsync(string login, string password)
        {
            var loginUri = "/auth/sign_in";

            var loginJsonString = new {email = login, password = password};
            string jsonLogin = JsonConvert.SerializeObject(loginJsonString);

            var result = await _httpClient.PostAsync($@"{_configuration.BaseUrl}\{loginUri}",
                new StringContent(jsonLogin, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                var key = result.Headers.GetValues("access-token").First();
                var uid = result.Headers.GetValues("uid").First();
                var client2 = result.Headers.GetValues("client").First();

                var jsonBodyResult = await result.Content.ReadAsStringAsync();

                var authenticationResultUser = AuthenticationResultBody.FromJson(jsonBodyResult);

                return new AuthenticationResult()
                {
                    AccessToken = key,
                    Client = client2,
                    Uid = uid,
                    User = authenticationResultUser.User
                };
            }


            return null;
        }
    }
}