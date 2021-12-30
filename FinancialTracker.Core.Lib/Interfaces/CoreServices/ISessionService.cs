using FinanceTrackerAPI.Models.DTOs;
using System.Net.Http;

namespace FinancialTracker.Core.Lib.CoreServices
{
    public interface ISessionService
    {
        void SetTokens(HttpResponseMessage response);
        void ClearTokens();
        string GetAccessToken();
        void SetUsername(string username);
        string GetUsername();
    }
}
