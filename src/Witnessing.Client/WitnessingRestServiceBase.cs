using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Witnessing.Client.Model.Contract;

namespace Witnessing.Client
{
    public class WitnessingRestServiceBase : RestServiceBase
    {
        private readonly IAuthenticationService _authenticationService;

        protected string ServiceName { get; set; }
        private bool _IsAuthenticated = false;

        public WitnessingRestServiceBase(IAuthenticationService authenticationService, HttpClient httpClient,
            ServiceConfiguration configuration) 
            : base(httpClient, configuration)
        {
            _authenticationService = authenticationService;
            
        }

        public WitnessingRestServiceBase(IAuthenticationService authenticationService, ServiceConfiguration configuration) 
            : base(configuration)
        {
            _authenticationService = authenticationService;
            
        }

        protected string GetBaseUrl()
        {
            if(string.IsNullOrEmpty(ServiceName))
                throw new ArgumentException($"{nameof(ServiceName)} can't be empty");

            if (string.IsNullOrEmpty(_configuration.WitnessingId))
                throw new ArgumentException($"{nameof(_configuration.WitnessingId)} can't be empty");

            return $@"{_configuration.BaseUrl}/{ServiceName}/{_configuration.WitnessingId}";
        }

        public async Task<bool> CheckResultIsOk(HttpResponseMessage responseMessage, [CallerMemberName] string callerName = "")
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }

            if (responseMessage.StatusCode != HttpStatusCode.Unauthorized)
            {
                _IsAuthenticated = false;
            }

            if (responseMessage.StatusCode != HttpStatusCode.NotFound)
            {
                throw new WitnessingServiceException(
                    $"Error in {callerName}: StatusCode: {responseMessage.StatusCode} Message: {await responseMessage.Content.ReadAsStringAsync()}");

            }

            return false;
        }

        protected async Task AuthenticateAsync()
        {
            if(!_IsAuthenticated)
            {
                var authData = await _authenticationService.LoginAsync(_configuration.Login,_configuration.Password);

                SetAuthHeaders(authData);

                _IsAuthenticated = true;
            }
        }

        protected void SetAuthHeaders(AuthData authentication)
        {

            var accessToken = "access-token";

            if (_httpClient.DefaultRequestHeaders.All(h => h.Key != accessToken))
            {
                _httpClient.DefaultRequestHeaders.Add(accessToken, authentication.AccessToken);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Remove(accessToken);

                _httpClient.DefaultRequestHeaders.Add(accessToken, authentication.AccessToken);
            }

            var uid = "uid";

            if (_httpClient.DefaultRequestHeaders.All(h => h.Key != uid))
            {
                _httpClient.DefaultRequestHeaders.Add(uid, authentication.Uid);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Remove(uid);
                _httpClient.DefaultRequestHeaders.Add(uid, authentication.Uid);
            }

            var client = "client";

            if (_httpClient.DefaultRequestHeaders.All(h => h.Key != client))
            {
                _httpClient.DefaultRequestHeaders.Add(client, authentication.Client);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Remove(client);
                _httpClient.DefaultRequestHeaders.Add(client, authentication.Client);
            }
        }

    }
}