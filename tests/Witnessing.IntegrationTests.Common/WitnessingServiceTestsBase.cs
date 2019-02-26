using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Tests;
using Witnessing.Client;
using Witnessing.Client.Model.Contract;

namespace Witnessing.IntegrationTests.Common
{
    public class WitnessingServiceTestsBase
    {
        protected ServiceConfiguration _conf;
        protected IAuthenticationService _authData;

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
