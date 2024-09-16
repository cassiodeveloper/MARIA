namespace Maria.Application.Accounts;

public record TokenResponse
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public string Token { get; init; }
}
