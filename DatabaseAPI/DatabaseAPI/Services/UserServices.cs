using DatabaseAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAPI.Services
{
    public class UserServices : IUserServices
    {
        IUserRepository _userRepository;
        ICashierRepository _cashierRepository;
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public UserServices(IUserRepository userRepository, ICashierRepository cashierRepository)
        {
            _userRepository = userRepository;
            _cashierRepository = cashierRepository;
        }

        public Task<Guid?> GetCashier(string username, string password, string cashierName)
        {
            return Task.Run<Guid?>(() =>
            {
                var user = _userRepository.GetUser(username, ComputeSha256Hash(password));
                if (user is null)
                    return null;
                var result = _cashierRepository.GetCashierKey(cashierName);
                return result == Guid.Empty ? null : result;
            });
        }

        public Task<string?> LogIn(string username, string password, DatabaseModels.UserType userType)
        {
            return Task.Run(() =>
            {
                var user = _userRepository.GetUser(username, ComputeSha256Hash(password));
                if (user is null || user.Type != userType)
                    return null;
                byte[] rngBytes = new byte[24];
                rngCsp.GetBytes(rngBytes);
                string sessionId = Convert.ToBase64String(rngBytes);
                Startup.ActiveSessions.Add(sessionId, user);
                return sessionId;
            });
        }

        public Task<bool> LogOut(string sessionId)
        {
            return Task.Run(() =>
            {
                if (Startup.ActiveSessions.ContainsKey(sessionId))
                    Startup.ActiveSessions.Remove(sessionId);
                return Startup.ActiveSessions.ContainsKey(sessionId);
            });
        }

        #region CRUD
        public Task<DatabaseModels.User> PostUser(DatabaseModels.User user)
        {
            return Task.Run(() =>
            {
                return _userRepository.Insert(new DatabaseModels.User(Guid.NewGuid(), user.Username, ComputeSha256Hash(user.PasswordHash), user.Type));
            });
        }

        public Task<bool> DeleteUser(Guid guid)
        {
            return Task.Run(() =>
            {
                return _userRepository.Delete(new DatabaseModels.User(guid, "a", "a", DatabaseModels.UserType.Admin));
            });
        }

        public Task<DatabaseModels.User> UpdateUser(DatabaseModels.User user)
        {
            return Task.Run(() =>
            {
                return _userRepository.Update(user);
            });
        }

        public Task<IEnumerable<DatabaseModels.User>> GetUsers()
        {
            return Task.Run(() => { return _userRepository.Get(); });
        }

        public Task<DatabaseModels.User?> GetUser(Guid id)
        {
            return Task.Run(() =>
            {
                var collection = _userRepository.Get().Where(x => x.Id == id);
                if (collection.Count() == 1)
                    return collection.Single();
                else
                    return null;
            });
        }
        #endregion

        string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.Default.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
