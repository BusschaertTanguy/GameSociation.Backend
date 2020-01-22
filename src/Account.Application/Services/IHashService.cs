namespace Account.Application.Services
{
    public interface IHashService
    {
        string Hash(string value);
        bool Compare(string originalValue, string valueToCompare);
    }
}
