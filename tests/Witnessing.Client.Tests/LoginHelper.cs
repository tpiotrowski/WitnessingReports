using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class LoginHelper
    {
        public static (string login, string password) GetLoginAndPassword()
        {
            var path = TestContext.CurrentContext.TestDirectory + @"\login.json";

            using (StreamReader jsonFileReader = new StreamReader(path))
            {
                var readToEnd = jsonFileReader.ReadToEnd();

                var deserializeObject = JsonConvert.DeserializeObject<dynamic>(readToEnd);

                return (deserializeObject.Username, deserializeObject.Password);
            }
        }
    }
}