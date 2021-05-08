using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.DatabaseModels
{
    public enum UserType
    {
        Admin,
        Manager
    }
    public class User : IDbModel
    {
        public Guid Id { get; }
        public string Username { get; }
        public string PasswordHash { get; set; }
        public UserType Type { get; set; }
    }
}
