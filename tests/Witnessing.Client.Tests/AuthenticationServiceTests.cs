using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Witnessing.Client;

namespace Tests
{
    /// <summary>
    /// To work properly please add json file with name login.json
    /// {
    /// "Username": "login",
    /// "Password": "passwd"
    /// }
    /// </summary>
    public class AuthenticationServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LoginUserTest()
        {
            var loginAndPassword = LoginHelper.GetLoginAndPassword();

            using (AuthenticationService authentication = new AuthenticationService(new AuthServiceConfiguration()))
            {
                var authenticationResult =
                    await authentication.LoginAsync(loginAndPassword.login, loginAndPassword.password);

                Assert.Multiple(() =>
                {
                    Assert.That(authenticationResult, Is.Not.Null);
                    Assert.That(authenticationResult.User, Is.Not.Null);
                    Assert.That(authenticationResult.User.Email, Is.EqualTo(loginAndPassword.login));
                    Assert.That(authenticationResult.AccessToken, Is.Not.Null.Or.Empty);
                    Assert.That(authenticationResult.Client, Is.Not.Null.Or.Empty);
                    Assert.That(authenticationResult.Uid, Is.Not.Null.Or.Empty);
                });
            }
        }
    }


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