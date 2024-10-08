using Maria.Core.Accounts;

namespace Maria.Application.Accounts;

public record CreateUserRequest
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string PasswordCheck { get; init; }

    internal User ToDomain()
    {
        return new User { Name = Name, Email = Email };
    }

    public bool PasswordCheckVerify()
    {
        return Password == PasswordCheck;
    }
}
