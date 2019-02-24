using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Witnessing.Client.DataModel;

namespace Witnessing.Client
{
    public class WitnessingService : WitnessingRestServiceBase
    {
        protected string _apiSubstring = String.Empty;

        public WitnessingService(AuthData authData, HttpClient httpClient, ServiceConfiguration configuration)
            : base(authData, httpClient, configuration)
        {
            ServiceName = "witnessings";
        }

        public WitnessingService(AuthData authData, ServiceConfiguration configuration) : base(authData, configuration)
        {
            ServiceName = "witnessings";
        }

        public async Task<bool> CheckResultIsOk(HttpResponseMessage responseMessage, [CallerMemberName] string callerName = "")
        {
            if (responseMessage.IsSuccessStatusCode)
                return true;
            if(responseMessage.StatusCode != HttpStatusCode.NotFound)
                throw new WitnessingServiceException(
                $"Error in {callerName}: StatusCode: {responseMessage.StatusCode} Message: {await responseMessage.Content.ReadAsStringAsync()}");

            return false;
        }

        public async Task<WitnessingMember[]> GetMembersAsync(int page = 1, int resultCount = 100, string filter = "")
        {
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
            var url =
                $@"{GetBaseUrl()}/days/{weekDayNumber}/hours";

            var witnessingMembersResponse = await _httpClient.GetAsync(url);

            if (await CheckResultIsOk(witnessingMembersResponse))
            {
                var responseVal = await witnessingMembersResponse.Content.ReadAsStringAsync();

                var fromJson = Hours.FromJson(responseVal);


                return fromJson.WitnessingHours;
            }

            return new WitnessingHour[]{};
        }

        public async Task<SortedList<int,WitnessingHour[]>> GetHoursForWeekAsync()
        {
            var weekDays = Enumerable.Range(1, 7);
            SortedList<int, WitnessingHour[]> result = new SortedList<int, WitnessingHour[]>();
            foreach (var weekDay in weekDays)
            {
                
                    var hoursForWeekDay = await GetHoursAsync(weekDay);
                    result.Add(weekDay, hoursForWeekDay);

            }

            return result;
        }


        public async Task<WitnessingLocation[]> GetLocationsAsync(int page = 1, int resultCount = 100, string filter = "")
        {
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

        public async Task<DispositionUser[]> GetDispositionAsync(DateTime date, long hourId)
        {

            //https://wielkomiejskie.org/api/v1/witnessings/11/schedule/dispositions?date=2019-03-13&hour_id=263&search_query=
            var url =
                $@"{GetBaseUrl()}/schedule/dispositions?date={date.Year}-{date.Month:D2}-{date.Day:D2}&hour_id={hourId}&search_query=";

            var witnessingMembersResponse = await _httpClient.GetAsync(url);

            if (await CheckResultIsOk(witnessingMembersResponse))
            {
                var responseVal = await witnessingMembersResponse.Content.ReadAsStringAsync();

                var fromJson = Dispositions.FromJson(responseVal);


                return fromJson.Users;
            }

            return null;
        }


        //https://wielkomiejskie.org/api/v1/witnessings/11/days/1/hours


        
    }
}