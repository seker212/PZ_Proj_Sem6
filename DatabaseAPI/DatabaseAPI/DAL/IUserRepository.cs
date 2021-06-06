using DatabaseAPI.DatabaseModels;

namespace DatabaseAPI.DAL
{
    public interface IUserRepository
    {
        User GetUser(string username, string hashed_password);
    }
}