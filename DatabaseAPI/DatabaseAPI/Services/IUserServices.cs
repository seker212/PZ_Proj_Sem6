using DatabaseAPI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public interface IUserServices
    {
        Task<bool> DeleteUser(Guid guid);
        Task<Guid?> GetCashier(string username, string password, string cashierName);
        Task<User> GetUser(Guid id);
        Task<IEnumerable<User>> GetUsers();
        Task<string> LogIn(string username, string password, UserType userType);
        Task<bool> LogOut(string sessionId);
        Task<User> PostUser(User user);
        Task<User> UpdateUser(User user);
    }
}