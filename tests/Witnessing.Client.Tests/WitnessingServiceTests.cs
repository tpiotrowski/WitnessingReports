using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Witnessing.Client;
using Witnessing.Client.DataModel;

namespace Tests
{
    [TestFixture]
    public class WitnessingServiceTests : WitnessingServiceTestsBase
    {
        [Test]
        public async Task GetMembersTest()
        {
            await RunTestAsAuthenticated(async (auth, conf) =>
            {
                WitnessingService ws = new WitnessingService(auth, conf);


                var members = await ws.GetMembersAsync();

                Assert.Multiple(() =>
                {
                    Assert.That(members, Is.Not.Null);
                    Assert.That(members, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task GetHoursTest()
        {
            await RunTestAsAuthenticated(async (auth, conf) =>
            {
                WitnessingService ws = new WitnessingService(auth, conf);

                var members = await ws.GetHoursAsync(1);

                Assert.Multiple(() =>
                {
                    Assert.That(members, Is.Not.Null);
                    Assert.That(members, Is.Not.Empty);
                });
            });
        }


        [Test]
        public async Task GetLocationTest()
        {
            await RunTestAsAuthenticated(async (auth, conf) =>
            {
                WitnessingService ws = new WitnessingService(auth, conf);

                var members = await ws.GetLocationsAsync();

                Assert.Multiple(() =>
                {
                    Assert.That(members, Is.Not.Null);
                    Assert.That(members, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task GetScheduleTest()
        {
            await RunTestAsAuthenticated(async (auth, conf) =>
            {
                WitnessingService ws = new WitnessingService(auth, conf);

                var members = await ws.GetScheduleAsync(DateTime.Parse("25-02-2019"));

                Assert.Multiple(() =>
                {
                    Assert.That(members, Is.Not.Null);
                    Assert.That(members, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task GetScheduleForMonthTest()
        {
            await RunTestAsAuthenticated(async (auth, conf) =>
            {
                WitnessingService ws = new WitnessingService(auth, conf);

                var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, 2);
                var lookupDate = new DateTime(DateTime.Now.Year, 2, 1);


                List<WitnessingScheduleMember> _members = new List<WitnessingScheduleMember>();

                for (int i = 2; i <= daysInMonth; i++)
                {
                    var members = await ws.GetScheduleAsync(lookupDate);
                    _members.AddRange(members);
                    lookupDate = lookupDate.AddDays(1);
                }

                Assert.Multiple(() =>
                {
                    Assert.That(_members, Is.Not.Null);
                    Assert.That(_members, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task GetDispositionsTest()
        {
            await RunTestAsAuthenticated(async (auth, conf) =>
            {
                WitnessingService ws = new WitnessingService(auth, conf);

                var lookupDate = new DateTime(DateTime.Now.Year, 3, 13);

                List<DispositionUser> _members = new List<DispositionUser>();
                var members = await ws.GetDispositionAsync(lookupDate, 301);//11:00
                _members.AddRange(members);


                Assert.Multiple(() =>
                {
                    Assert.That(_members, Is.Not.Null);
                    Assert.That(_members, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task GetDispositionsForMonthTest()
        {
            await RunTestAsAuthenticated(async (auth, conf) =>
            {
                WitnessingService ws = new WitnessingService(auth, conf);

                var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, 3);
                var lookupDate = new DateTime(DateTime.Now.Year, 3, 1);

                var hoursForWeekAsync = await ws.GetHoursForWeekAsync();

                List<DispositionUser> _members = new List<DispositionUser>();

                for (int i = 2; i <= daysInMonth; i++)
                {
                    var dayOfWeek = ((int) lookupDate.DayOfWeek == 0 ? 7 : (int) lookupDate.DayOfWeek);

                    if (hoursForWeekAsync.TryGetValue(dayOfWeek, out WitnessingHour[] hours))
                    {
                        foreach (var witnessingHour in hours)
                        {
                            var members = await ws.GetDispositionAsync(lookupDate, witnessingHour.Id);
                            _members.AddRange(members);
                        }
                    }

                    lookupDate = lookupDate.AddDays(1);
                }

                Assert.Multiple(() =>
                {
                    Assert.That(_members, Is.Not.Null);
                    Assert.That(_members, Is.Not.Empty);
                });
            });
        }
    }
}