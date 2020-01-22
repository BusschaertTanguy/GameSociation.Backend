namespace Account.Application.Services
{
    public interface ITokenService
    {
        string Generate(string email);
    }
}