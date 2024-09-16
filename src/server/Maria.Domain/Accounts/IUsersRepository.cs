namespace Maria.Core.Accounts;

public interface IUsersRepository
{
    Task<User> GetUserByEmail(string email);
    ValueTask<int> CreateUser(User user);
}