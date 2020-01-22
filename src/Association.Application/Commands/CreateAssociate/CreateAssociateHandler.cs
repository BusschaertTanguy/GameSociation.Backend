using System.Threading;
using System.Threading.Tasks;
using Association.Application.Services;
using Association.Domain.Entities;
using Association.Domain.Repositories;
using Common.Application.Commands;

namespace Association.Application.Commands.CreateAssociate
{
    public class CreateAssociateHandler : ICommandHandler<CreateAssociate>
    {
        private readonly IAssociateRepository _associateRepository;
        private readonly ITagService _tagService;

        public CreateAssociateHandler(IAssociateRepository associateRepository, ITagService tagService)
        {
            _associateRepository = associateRepository;
            _tagService = tagService;
        }

        public async Task Handle(CreateAssociate notification, CancellationToken cancellationToken)
        {
            var tagNumber = _tagService.GenerateTagNumber(notification.Username);
            var associate = new Associate(notification.AccountId, notification.Username, tagNumber);
            await _associateRepository.Save(associate).ConfigureAwait(false);
        }
    }
}