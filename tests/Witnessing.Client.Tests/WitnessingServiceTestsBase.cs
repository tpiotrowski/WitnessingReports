using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Witnessing.Client;
using Witnessing.Client.Model.Contract;

namespace Tests
{
    public class WitnessingServiceTestsBase
    {
        private ServiceConfiguration _conf;
        private IAuthenticationService _authData;

        [OneTimeSetUp]
        protected async Task SetupFixture()
        {
            var loginAndPassword = LoginHelper.GetLoginAndPassword();

            var serviceConfiguration = new ServiceConfiguration();
            serviceConfiguration.WitnessingId = "11";
            serviceConfiguration.Login = loginAndPassword.login;
            serviceConfiguration.Password = loginAndPassword.password;


            AuthenticationService authentication = new AuthenticationService(serviceConfiguration);
            
            _conf = serviceConfiguration;
            _authData = authentication;
        }


        public async Task RunTestAsAuthenticated(Func<IAuthenticationService, ServiceConfiguration, Task> runTestAction)
        {
            await runTestAction(_authData, _conf);
        }
    }
}