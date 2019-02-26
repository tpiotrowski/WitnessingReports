using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Witnessing.Client.DataModel;

namespace Witnessing.Client.Model.Contract
{
    public interface IWitnessingService
    {
        Task<bool> CheckResultIsOk(HttpResponseMessage responseMessage, [CallerMemberName] string callerName = "");
        Task<WitnessingMember[]> GetMembersAsync(int page = 1, int resultCount = 100, string filter = "");
        Task<WitnessingHour[]> GetHoursAsync(int weekDayNumber);
        Task<SortedList<int, WitnessingHour[]>> GetHoursForWeekAsync();
        Task<WitnessingLocation[]> GetLocationsAsync(int page = 1, int resultCount = 100, string filter = "");
        Task<WitnessingScheduleMember[]> GetScheduleAsync(DateTime date);
        Task<DispositionUser[]> GetDispositionAsync(DateTime date, long hourId);
        Task<DispositionUser[]> GetDispositionForMonthAsync(int year, int month);
        Task<DispositionUser[]> GetDispositionForDayAsync(DateTime date);
        void Dispose();
    }

    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string login, string password);
    }
}