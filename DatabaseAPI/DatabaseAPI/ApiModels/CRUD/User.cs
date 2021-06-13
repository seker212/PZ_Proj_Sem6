using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAPI.ApiModels.CRUD
{
    public class User
    {
        public User(Guid id, string username, string passwordHash, DatabaseModels.UserType type)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Type = type;
        }

        public Guid Id { get; }
        public string Username { get; }
        public string PasswordHash { get; }
        public DatabaseModels.UserType Type { get; }
    }
}
