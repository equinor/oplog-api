using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
