using Maria.Core.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Maria.Infrastructure.Database;

public class MariaDbContext: DbContext
{
    public MariaDbContext(DbContextOptions<MariaDbContext> options) : base(options) 
    { 
        
    }

    public DbSet<User> Users { get; set; }
}
