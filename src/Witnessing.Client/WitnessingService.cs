using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Witnessing.Client.DataModel;
using Witnessing.Client.Model.Contract;

namespace Witnessing.Client
{
    public class WitnessingService : WitnessingRestServiceBase, IWitnessingService
    {
        protected string _apiSubstring = String.Empty;


        public WitnessingService(IAuthenticationService authData, HttpClient httpClient,
            ServiceConfiguration configuration)
            : base(authData, httpClient, configuration)
        {
            ServiceName = "witnessings";
        }

        public WitnessingService(IAuthenticationService authData, ServiceConfiguration configuration)
            : base(authData, configuration)
        {
            ServiceName = "witnessings";
        }


        public async Task<WitnessingMember[]> GetMembersAsync(int page = 1, int resultCount = 100, string filter = "")
        {
            await AuthenticateAsync();

            var url =
                $@"{GetBaseUrl()}/members?page={page}&per_page={resultCount}&order=name&direction=asc&search_query={filter}";

            var witnessingMembersResponse = await _httpClient.GetAsync(url);

            if (await CheckResultIsOk(witnessingMembersResponse))
            {
                var responseVal = await witnessingMembersResponse.Content.ReadAsStringAsync();

                var fromJson = Members.FromJson(responseVal);


                return fromJson.WitnessingMembers;
            }

            return null;
        }


        public async Task<WitnessingHour[]> GetHoursAsync(int weekDayNumber)
        {
            await AuthenticateAsync();

            var url =
                $@"{GetBaseUrl()}/days/{weekDayNumber}/hours";

            var witnessingMembersResponse = await _httpClient.GetAsync(url);

            if (await CheckResultIsOk(witnessingMembersResponse))
            {
                var responseVal = await witnessingMembersResponse.Content.ReadAsStringAsync();

                var fromJson = Hours.FromJson(responseVal);


                return fromJson.WitnessingHours;
            }

            return new WitnessingHour[] { };
        }

        public async Task<SortedList<int, WitnessingHour[]>> GetHoursForWeekAsync()
        {
            await AuthenticateAsync();
            var weekDays = Enumerable.Range(1, 7);
            SortedList<int, WitnessingHour[]> result = new SortedList<int, WitnessingHour[]>();

            foreach (var weekDay in weekDays)
            {
                var hoursForWeekDay = await GetHoursAsync(weekDay);
                result.Add(weekDay, hoursForWeekDay);
            }

            return result;
        }


        public async Task<WitnessingLocation[]> GetLocationsAsync(int page = 1, int resultCount = 100,
            string filter = "")
        {
            await AuthenticateAsync();

            var url =
                $@"{GetBaseUrl()}/locations?page={page}&per_page={resultCount}&order=name&direction=asc&search_query={filter}";

            var witnessingMembersResponse = await _httpClient.GetAsync(url);

            if (await CheckResultIsOk(witnessingMembersResponse))
            {
                var responseVal = await witnessingMembersResponse.Content.ReadAsStringAsync();

                if (responseVal != null)
                {
                    var fromJson = Locations.FromJson(responseVal);

                    return fromJson.WitnessingLocations;
                }
            }

            return null;
        }

        public async Task<WitnessingScheduleMember[]> GetScheduleAsync(DateTime date)
        {
            await AuthenticateAsync();

            var url =
                $@"{GetBaseUrl()}/schedule?date={date:d}";

            var witnessingMembersResponse = await _httpClient.GetAsync(url);

            if (await CheckResultIsOk(witnessingMembersResponse))
            {
                var responseVal = await witnessingMembersResponse.Content.ReadAsStringAsync();

                var fromJson = Schedule.FromJson(responseVal);


                return fromJson.WitnessingScheduleMembers;
            }

            return null;
        }


        private WitnessingMember[] cachedMembers = null;
        public async Task<DispositionUser[]> GetDispositionAsync(DateTime date, long hourId)
        {
            await AuthenticateAsync();
            //https://wielkomiejskie.org/api/v1/witnessings/11/schedule/dispositions?date=2019-03-13&hour_id=263&search_query=
            var url =
                $@"{GetBaseUrl()}/schedule/dispositions?date={date.Year}-{date.Month:D2}-{date.Day:D2}&hour_id={hourId}&search_query=";

            var witnessingMembersResponse = await _httpClient.GetAsync(url);

            if (await CheckResultIsOk(witnessingMembersResponse))
            {
                var responseVal = await witnessingMembersResponse.Content.ReadAsStringAsync();

                var fromJson = Dispositions.FromJson(responseVal);


                var fromJsonUsers = fromJson.Users;


                var schedulesForThisDay = await GetScheduleAsync(date);

                var witnessingScheduleMembers = schedulesForThisDay.Where(s => s.HourId == hourId).ToList();

                if (cachedMembers == null)
                {
                    cachedMembers = await GetMembersAsync();
                }

                foreach (var witnessingScheduleMember in witnessingScheduleMembers)
                {
                    DispositionUser h = new DispositionUser();

                    var witnessingMember = cachedMembers.Single(m => m.Id == witnessingScheduleMember.UserId);

                    h.Date = witnessingScheduleMember.Date.DateTime;
                    h.HourId = witnessingScheduleMember.HourId;
                    h.FirstName = witnessingMember.FirstName;
                    h.LastName = witnessingMember.LastName;
                    h.Email = witnessingMember.Email;
                    var fromJsonUsersTmp = fromJsonUsers.ToList();
                    fromJsonUsersTmp.Add(h);

                    fromJsonUsers = fromJsonUsersTmp.ToArray();
                }

                foreach (var dispositionUser in fromJsonUsers)
                {
                    dispositionUser.HourId = hourId;
                    dispositionUser.Date = date;
                }


                return fromJsonUsers;
            }

            return null;
        }


        public async Task<DispositionUser[]> GetDispositionForDayAsync(DateTime date)
        {
            var weekDay = (int) date.DayOfWeek == 0 ? 7 : (int) date.DayOfWeek;
            var hours = await GetHoursAsync(weekDay);

            List<DispositionUser> _members = new List<DispositionUser>();


            foreach (var witnessingHour in hours)
            {
                var dispositions = await GetDispositionAsync(date, witnessingHour.Id);
                _members.AddRange(dispositions);
            }

            return _members.ToArray();
        }


        public async Task<DispositionUser[]> GetDispositionForMonthAsync(int year, int month)
        {
            if (month <= 0 || month > 12) throw new ArgumentOutOfRangeException(nameof(month));

            if (year > DateTime.Now.Year) throw new ArgumentOutOfRangeException(nameof(year));


            await AuthenticateAsync();

            var hoursForWeekAsync = await GetHoursForWeekAsync();

            var daysInMonth = DateTime.DaysInMonth(year, month);
            var lookupDate = new DateTime(year, month, 1);

            List<DispositionUser> _members = new List<DispositionUser>();

            for (int i = 1; i <= daysInMonth; i++)
            {
                var dayOfWeek = ((int) lookupDate.DayOfWeek == 0 ? 7 : (int) lookupDate.DayOfWeek);

                if (hoursForWeekAsync.TryGetValue(dayOfWeek, out WitnessingHour[] hours))
                {
                    foreach (var witnessingHour in hours)
                    {
                        var members = await GetDispositionAsync(lookupDate, witnessingHour.Id);
                        _members.AddRange(members);
                    }
                }

                lookupDate = lookupDate.AddDays(1);
            }

            return _members.ToArray();
        }


        //https://wielkomiejskie.org/api/v1/witnessings/11/days/1/hours
    }
}