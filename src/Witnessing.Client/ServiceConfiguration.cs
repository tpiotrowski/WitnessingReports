namespace Witnessing.Client
{
    public class ServiceConfiguration
    {
        public string HostUrl { get; set; } = "https://wielkomiejskie.org";
        public string ApiUrl { get; set; } = "api/v1";
        public string BaseUrl => $@"{HostUrl}/{ApiUrl}";
        public string WitnessingId { get; set; }
    }
}