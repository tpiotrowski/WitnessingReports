using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Witnessing.Client
{
    public abstract class RestServiceBase : IDisposable
    {
        protected readonly ServiceConfiguration _configuration;
        protected HttpClient _httpClient = null;

        protected RestServiceBase(HttpClient httpClient, ServiceConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            ConfigureHttpClient();
        }

        protected RestServiceBase(ServiceConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            ConfigureHttpClient();
        }

        protected void ConfigureHttpClient()
        {
            var mediaTypeWithQualityHeaderValue = new MediaTypeWithQualityHeaderValue("application/json");

            if(!_httpClient.DefaultRequestHeaders.Accept.Contains(mediaTypeWithQualityHeaderValue))
                _httpClient.DefaultRequestHeaders.Accept.Add(mediaTypeWithQualityHeaderValue);
        }

        
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}