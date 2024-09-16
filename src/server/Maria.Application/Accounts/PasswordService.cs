namespace Maria.Application.Accounts;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}

public class PasswordService : IPasswordService
{
    private const string GLOBAL_SALT = "=2~D49I(TGmf";

    public string HashPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        return BCrypt.Net.BCrypt.HashPassword(password + GLOBAL_SALT, salt);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password + GLOBAL_SALT, hashedPassword);
    }
}
