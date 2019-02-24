using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Witnessing.Client;

namespace Tests
{
    public class WitnessingServiceTestsBase
    {

        private ServiceConfiguration _conf;
        private AuthData _authData;

        [OneTimeSetUp]
        protected async Task SetupFixture()
        {
            var loginAndPassword = LoginHelper.GetLoginAndPassword();

            var serviceConfiguration = new ServiceConfiguration();
            serviceConfiguration.WitnessingId = "11";

            using (AuthenticationService authentication = new AuthenticationService(serviceConfiguration))
            {
                var authenticationResult =
                    await authentication.LoginAsync(loginAndPassword.login, loginAndPassword.password);

                _conf = serviceConfiguration;
                _authData = authenticationResult;
            }

        }


        public async Task RunTestAsAuthenticated(Func<AuthData, ServiceConfiguration, Task> runTestAction)
        {
            await runTestAction(_authData, _conf);            
        }
    }
}