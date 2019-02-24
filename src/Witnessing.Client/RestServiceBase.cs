using System;
using System.Net.Http;

namespace Witnessing.Client
{
    public abstract class RestServiceBase : IDisposable
    {
        protected readonly AuthServiceConfiguration _configuration;
        protected HttpClient _httpClient = null;

        protected RestServiceBase(HttpClient httpClient, AuthServiceConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        protected RestServiceBase(AuthServiceConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }
        
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}