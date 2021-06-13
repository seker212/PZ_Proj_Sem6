using DatabaseAPI.DatabaseModels;

namespace DatabaseAPI.DAL
{
    public interface IUserRepository : IObjectRepository<User>
    {
        User GetUser(string username, string hashed_password);
    }
}