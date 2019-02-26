using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Witnessing.Client.DataModel;
using Witnessing.Client.Model.Contract;
using Witnessing.Data.Contract;
using Witnessing.Data.Model;
using WitnessingMember = Witnessing.Data.Model.WitnessingMember;

namespace Witnessing.Data.Service
{
    public class WitnessingDataService : IWitnessingDataService
    {
        private readonly IWitnessingService _witnessingService;

        public WitnessingDataService(IWitnessingService witnessingService)
        {
            _witnessingService = witnessingService;
        }

        public async Task<WitnessingMember[]> GetMembersAsync(int page = 1, int resultCount = 100, string filter = "")
        {
            var members = await _witnessingService.GetMembersAsync(page, resultCount, filter);

            return members.Select(m => new WitnessingMember()
            {
                Id = m.Id,
                Email = m.Email,
                LastName = m.LastName,
                Name = m.FirstName,
                Phone = m.Phone
            }).ToArray();
        }

        public async Task<Hour[]> GetHoursAsync(int weekDayNumber)
        {
            var hours = await _witnessingService.GetHoursAsync(weekDayNumber);

            List<Hour> hoursResult = new List<Hour>();

            foreach (var witnessingHour in hours)
            {
                var hour = GetHour(witnessingHour);

                hoursResult.Add(hour);
            }

            return hoursResult.ToArray();
        }

        private static Hour GetHour(WitnessingHour witnessingHour)
        {
            var hour = new Hour();

            var dayOfWeek = (int) witnessingHour.DayId == 7 ? 0 : (int) witnessingHour.DayId;

            hour.DayOfWeek = (DayOfWeek) dayOfWeek;

            var startTime = hour.TimeOfDay = TimeSpan.Parse(witnessingHour.Start);

            var endTime = TimeSpan.Parse(witnessingHour.End);

            hour.Duration = endTime - startTime;

            hour.Id = witnessingHour.Id;
            return hour;
        }

        public async Task<SortedList<int, Hour[]>> GetHoursForWeekAsync()
        {
            var hours = await _witnessingService.GetHoursForWeekAsync();
            SortedList<int, Hour[]> hoursResult = new SortedList<int, Hour[]>();


            foreach (var hoursKey in hours.Keys)
            {
                var hoursFoKey = hours[hoursKey];

                var hoursToInsert = hoursFoKey.Select(GetHour).ToArray();

                hoursResult.Add(hoursKey, hoursToInsert);
            }

            return hoursResult;
        }

        public async Task<Location[]> GetLocationsAsync(int page = 1, int resultCount = 100, string filter = "")
        {
            var locations = await _witnessingService.GetLocationsAsync(page, resultCount, filter);

            return locations.Select(l => new Location()
            {
                Id = l.Id,
                Name = l.Name
            }).ToArray();
        }

        public async Task<Schedule[]> GetScheduleAsync(DateTime date)
        {
            var schedule = await _witnessingService.GetScheduleAsync(date);
            var locations = await GetLocationsAsync();
            var hours = await GetHoursForWeekAsync();
            var members = await GetMembersAsync();

            var schedules = GetSchedules(schedule, locations, members, hours);

            return schedules.ToArray();
        }

        public async Task<Disposition[]> GetDispositionForHourAsync(DateTime date, long hourId)
        {
            var dispositons = await _witnessingService.GetDispositionAsync(date, hourId);

            //var locations = await GetLocationsAsync();
            var dispositionsRes = await GetDispositions(dispositons);


            return dispositionsRes.ToArray();
        }

        private async Task<List<Disposition>> GetDispositions(DispositionUser[] dispositons)
        {
            var hours = await GetHoursForWeekAsync();
            var members = await GetMembersAsync();

            List<Disposition> dispositionsRes = new List<Disposition>();

            foreach (var disposition in dispositons)
            {
                var disp = new Disposition
                {
                    Hour = hours.SelectMany(h => h.Value).Single(h => h.Id == disposition.HourId),
                    Date = disposition.Date,
                    Member = members.Single(m =>
                        m.Email == disposition.Email && m.Name == disposition.FirstName &&
                        m.LastName == disposition.LastName)
                };


                dispositionsRes.Add(disp);
            }

            return dispositionsRes;
        }


        public async Task<Disposition[]> GetDispositionForMonthAsync(int year, int month)
        {
            var dispositons = await _witnessingService.GetDispositionForMonthAsync(year, month);

            var dispositionsRes = await GetDispositions(dispositons);


            return dispositionsRes.ToArray();


        }

        public async Task<Disposition[]> GetDispositionForDayAsync(DateTime date)
        {
            var dispositons = await _witnessingService.GetDispositionForDayAsync(date);

            var dispositionsRes = await GetDispositions(dispositons);


            return dispositionsRes.ToArray();



        }

        private static List<Schedule> GetSchedules(WitnessingScheduleMember[] schedule, Location[] locations,
            WitnessingMember[] members,
            SortedList<int, Hour[]> hours)
        {
            List<Schedule> schedules = new List<Schedule>();

            foreach (var witnessingScheduleMember in schedule)
            {
                var scheduleRes = new Schedule();

                scheduleRes.Location = locations.Single(l => witnessingScheduleMember.LocationId == l.Id);
                scheduleRes.Member = members.Single(m => witnessingScheduleMember.MemberKey == m.Id);
                scheduleRes.ScheduleDate = witnessingScheduleMember.Date;
                scheduleRes.Hour = hours.SelectMany(h => h.Value).Single(h => h.Id == witnessingScheduleMember.HourId);
                schedules.Add(scheduleRes);
            }

            return schedules;
        }
    }
}