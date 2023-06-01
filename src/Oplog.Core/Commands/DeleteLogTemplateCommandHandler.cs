using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands
{
    public class DeleteLogTemplateCommandHandler : ICommandHandler<DeleteLogTemplateCommand>
    {
        private readonly ILogTemplateRepository _templateRepository;
        public DeleteLogTemplateCommandHandler(ILogTemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }
        public async Task Handle(DeleteLogTemplateCommand command)
        {
            await _templateRepository.Delete(command.Id);
            await _templateRepository.Save();
        }
    }
}
