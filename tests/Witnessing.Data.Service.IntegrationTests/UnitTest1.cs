using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Witnessing.Client;
using Witnessing.Data.Model;
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
                var dispositions = await wds.GetDispositionForDayAsync(DateTime.Parse("2019-03-13"));

                var groupBy = dispositions.GroupBy(el => el.Member);

                Assert.Multiple(() =>
                {
                    Assert.That(dispositions, Is.Not.Null);
                    Assert.That(dispositions, Is.Not.Empty);
                });
            });
        }

        [Test]
        public async Task get_dispositions_month()
        {
            await RunInWitnessingServiceContext(async wds =>
            {
                var year = 2019;
                var month = 1;
                var dispositions = await wds.GetDispositionForMonthAsync(year, month);

                var groupBy = dispositions.GroupBy(el => el.Date);

                StringBuilder headerBuilder = new StringBuilder();

                headerBuilder.Append($"Głosiciel;");

                foreach (var grouping in groupBy)
                {
                    var date = grouping.Key;
                    headerBuilder.Append($"{date:d};");
                }

                StringBuilder rowBuilder = new StringBuilder();


                var members = await wds.GetMembersAsync();

                Dictionary<string, StringBuilder> allMembersDisctionary =
                    members.ToDictionary(k => $"{k.LastName} {k.Name}", v => new StringBuilder());


                foreach (var grouping in groupBy)
                {
                    var dispositionGroups = grouping.GroupBy(d => new {d.Member.LastName, d.Member.Name});

                    var membersWithDispositions =
                        dispositionGroups.ToDictionary(k => $"{k.Key.LastName} {k.Key.Name}", k => k.ToList());

                    foreach (var keyValuePair in allMembersDisctionary)
                    {
                        //var member = keyValuePair.Key;

                        var stringBuilder = keyValuePair.Value;

                        if (membersWithDispositions.TryGetValue(keyValuePair.Key, out List<Disposition> disposition))
                        {
                            var s = disposition
                                .Aggregate(new StringBuilder(), (sb, d) => sb.Append($"{d.Hour.TimeOfDay.Hours},"))
                                .ToString();
                            stringBuilder.Append(s.Remove(s.Length - 1));
                        }

                        stringBuilder.Append(";");
                    }
                }

                foreach (var stringBuilder in allMembersDisctionary)
                {
                    var row1 = $"{stringBuilder.Key};{stringBuilder.Value.ToString()}";

                    rowBuilder.AppendLine(row1.Remove(row1.Length - 1));
                }
                
                StringBuilder csv = new StringBuilder();
                
                var header = headerBuilder.ToString().Trim(' ');
                csv.AppendLine(header.Remove(header.Length - 1));
                var row = rowBuilder.ToString().Trim(' ');
                csv.AppendLine(row);

                var csvRes = csv.ToString();


                var path = "d:\\CSV_Witnessing\\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = $"Zgloszenia_{month:D2}_{year}.csv";

                using (StreamWriter sw = new StreamWriter(Path.Combine(path,filename)))
                {
                    await sw.WriteAsync(csvRes);

                }


                Assert.Multiple(() =>
                {
                    Assert.That(dispositions, Is.Not.Null);
                    Assert.That(dispositions, Is.Not.Empty);
                });
            });
        }
    }
}