using System;
using System.Linq;
using System.Net.Http;

namespace Witnessing.Client
{
    public class WitnessingRestServiceBase : RestServiceBase
    {

        protected string ServiceName { get; set; }

        public WitnessingRestServiceBase(AuthData authData, HttpClient httpClient,
            ServiceConfiguration configuration) 
            : base(httpClient, configuration)
        {
            SetAuthHeaders(authData);
        }

        public WitnessingRestServiceBase(AuthData authData, ServiceConfiguration configuration) 
            : base(configuration)
        {
            SetAuthHeaders(authData);
        }

        protected string GetBaseUrl()
        {
            if(string.IsNullOrEmpty(ServiceName))
                throw new ArgumentException($"{nameof(ServiceName)} can't be empty");

            if (string.IsNullOrEmpty(_configuration.WitnessingId))
                throw new ArgumentException($"{nameof(_configuration.WitnessingId)} can't be empty");

            return $@"{_configuration.BaseUrl}/{ServiceName}/{_configuration.WitnessingId}";
        }

        protected void SetAuthHeaders(AuthData authentication)
        {

            var accessToken = "access-token";

            if (_httpClient.DefaultRequestHeaders.All(h => h.Key != accessToken))
            {
                

                _httpClient.DefaultRequestHeaders.Add(accessToken, authentication.AccessToken);
            }

            var uid = "uid";

            if (_httpClient.DefaultRequestHeaders.All(h => h.Key != uid))
            {
                _httpClient.DefaultRequestHeaders.Add(uid, authentication.Uid);
            }

            var client = "client";

            if (_httpClient.DefaultRequestHeaders.All(h => h.Key != client))
            {
                _httpClient.DefaultRequestHeaders.Add(client, authentication.Client);
            }
        }

    }
}