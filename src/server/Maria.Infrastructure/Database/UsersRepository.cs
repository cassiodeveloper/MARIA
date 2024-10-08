using Maria.Core.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Maria.Infrastructure.Database;

public class UsersRepository : IUsersRepository
{
    private readonly MariaDbContext _context;

    public UsersRepository(MariaDbContext context)
    {
        _context = context;
    }

    public async ValueTask<int> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
