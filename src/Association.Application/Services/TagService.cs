using System.Linq;
using Association.Application.Views;
using Common.Application.Queries;

namespace Association.Application.Services
{
    public class TagService : ITagService
    {
        private readonly IQueryProcessor _queryProvider;

        public TagService(IQueryProcessor queryProvider)
        {
            _queryProvider = queryProvider;
        }

        public int GenerateTagNumber(string username)
        {
            var associateView = _queryProvider.Query<AssociateView>().Where(x => x.Tag.Username == username).OrderByDescending(x => x.Tag.Number).FirstOrDefault();
            return associateView?.Tag.Number + 1 ?? 1;
        }
    }
}