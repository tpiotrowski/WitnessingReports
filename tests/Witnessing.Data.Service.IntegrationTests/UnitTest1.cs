using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Witnessing.Client;
using Witnessing.Data.Service;
using Witnessing.IntegrationTests.Common;

namespace Tests
{

    public class WitnessingDataServiceBase : WitnessingServiceTestsBase
    {
        public async Task RunInWitnessingServiceContext(Func<WitnessingDataService, Task> runTestAction)
        {
            WitnessingService ws = new WitnessingService(_authData, _conf);

            WitnessingDataService wds = new WitnessingDataService(ws);

            await runTestAction(wds);
        }
    }


    public class WitnessingDataServiceTests : WitnessingDataServiceBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task get_members_test()
        {

            await RunInWitnessingServiceContext(async wds =>
            {
                var members = await wds.GetMembersAsync();

                Assert.Multiple(() =>
                {
                    Assert.That(members, Is.Not.Null);
                    Assert.That(members, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task getHoursTest()
        {

            await RunInWitnessingServiceContext(async wds =>
            {
                var hours = await wds.GetHoursAsync(7);

                Assert.Multiple(() =>
                {
                    Assert.That(hours, Is.Not.Null);
                    Assert.That(hours, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task get_hours_for_week()
        {

            await RunInWitnessingServiceContext(async wds =>
            {
                var hours = await wds.GetHoursForWeekAsync();

                Assert.Multiple(() =>
                {
                    Assert.That(hours, Is.Not.Null);
                    Assert.That(hours, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task get_locations()
        {

            await RunInWitnessingServiceContext(async wds =>
            {
                var locations = await wds.GetLocationsAsync();

                Assert.Multiple(() =>
                {
                    Assert.That(locations, Is.Not.Null);
                    Assert.That(locations, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task get_dispositions()
        {

            await RunInWitnessingServiceContext(async wds =>
            {
                //var locations = await wds.GetDispositionForHourAsync();

                Assert.Multiple(() =>
                {
                    Assert.That(locations, Is.Not.Null);
                    Assert.That(locations, Is.Not.Empty);
                });
            });
        }
    }
}

