using Newtonsoft.Json;

namespace Witnessing.Client.DataModel
{
    public static class Serialize
    {
        public static string ToJson(this AuthenticationResult self) => JsonConvert.SerializeObject(self, Witnessing.Client.DataModel.Converter.Settings);
    }
}