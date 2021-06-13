using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.DatabaseModels;
using SqlKata.Execution;
using SqlKata.Extensions;

namespace DatabaseAPI.DAL
{

    public class UserRepository : ObjectRepository<User>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString, "users", User.ColumnNames)
        {
        }

        public User GetUser(string username, string hashed_password) => Query().Where("username", username).Where("password_hash", hashed_password).First<User>();
    }
}
