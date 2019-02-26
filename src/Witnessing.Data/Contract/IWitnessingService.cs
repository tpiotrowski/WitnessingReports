using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Witnessing.Data.Model;

namespace Witnessing.Data.Contract
{
    public interface IWitnessingDataService
    {
       
        Task<WitnessingMember[]> GetMembersAsync(int page = 1, int resultCount = 100, string filter = "");
        Task<Hour[]> GetHoursAsync(int weekDayNumber);
        Task<SortedList<int, Hour[]>> GetHoursForWeekAsync();
        Task<Location[]> GetLocationsAsync(int page = 1, int resultCount = 100, string filter = "");
        Task<Schedule[]> GetScheduleAsync(DateTime date);
        Task<Disposition[]> GetDispositionForHourAsync(DateTime date, long hourId);
    
    }
    
}