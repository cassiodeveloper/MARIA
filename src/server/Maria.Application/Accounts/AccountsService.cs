using Maria.Core.Accounts;
using System.Security.Claims;

namespace Maria.Application.Accounts;

public interface IAccountsService
{
    ValueTask<int> CreateUser(CreateUserRequest userRequest);
    ValueTask<TokenResponse> Login(LoginRequest loginRequest);
}

public class AccountsService : IAccountsService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;

    public AccountsService(IUsersRepository usersRepository,
        IPasswordService passwordService,
        ITokenService tokenService)
    {
        _usersRepository = usersRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }
    public async ValueTask<int> CreateUser(CreateUserRequest userRequest)
    {
        User user = userRequest.ToDomain();
        user.HashedPassword = _passwordService.HashPassword(userRequest.Password);
        user.Id = await _usersRepository.CreateUser(user);
        return user.Id;
    }

    public async ValueTask<TokenResponse> Login(LoginRequest loginRequest)
    {
        var user = await _usersRepository.GetUserByEmail(loginRequest.Email);
        if (user == null)
            return null;

        var passCorrect = _passwordService.VerifyPassword(loginRequest.Password, user.HashedPassword);
        if (!passCorrect)
            return null;

        var claimsIdentity = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "user")
                });

        var token = _tokenService.GenerateJwt(claimsIdentity);

        return new TokenResponse { Id = user.Id, Nome = user.Name, Token = token };
    }
}
