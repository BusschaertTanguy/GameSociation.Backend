using Association.Domain.Entities;

namespace Association.Application.Services
{
    public interface ITagService
    {
        int GenerateTagNumber(string username);
    }
}
