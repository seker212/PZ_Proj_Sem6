using DatabaseAPI.DatabaseModels;
using System;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface IUserServices
    {
        Task<Guid?> GetCashier(string username, string password, string cashierName);
        Task<string> LogIn(string username, string password, UserType userType);
        Task<bool> LogOut(string sessionId);
    }
}