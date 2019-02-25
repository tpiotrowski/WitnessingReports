using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Witnessing.Client.Model.Contract;
using Witnessing.Data.Contract;
using Witnessing.Data.Model;

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
                Id =  m.Id,
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
                var hour = new Hour();

                var dayOfWeek = (int) witnessingHour.DayId == 7 ? 0 : (int) witnessingHour.DayId;

                hour.DayOfWeek = (DayOfWeek) dayOfWeek;

                var startTime = hour.TimeOfDay = TimeSpan.Parse(witnessingHour.Start);
                var endTime =  TimeSpan.Parse(witnessingHour.End);

                hour.Duration = endTime - startTime;

            }

            return hoursResult;

        }

        public Task<SortedList<int, Hour[]>> GetHoursForWeekAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Location[]> GetLocationsAsync(int page = 1, int resultCount = 100, string filter = "")
        {
            throw new NotImplementedException();
        }

        public Task<WitnessingMember[]> GetScheduleAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<Disposition[]> GetDispositionAsync(DateTime date, long hourId)
        {
            throw new NotImplementedException();
        }

    }
}
