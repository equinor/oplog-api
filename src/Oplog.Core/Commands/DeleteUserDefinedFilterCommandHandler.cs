using Oplog.Core.Infrastructure;
using Oplog.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Commands
{
    public class DeleteUserDefinedFilterCommandHandler : ICommandHandler<DeleteUserDefinedFilterCommand>
    {
        private readonly IUserDefinedFilterRepository _userDefinedFilterRepository;
        public DeleteUserDefinedFilterCommandHandler(IUserDefinedFilterRepository userDefinedFilterRepository)
        {
            _userDefinedFilterRepository = userDefinedFilterRepository;
        }

        public async Task Handle(DeleteUserDefinedFilterCommand command)
        {
            await _userDefinedFilterRepository.Delete(command.FilterId);
            await _userDefinedFilterRepository.Save();
        }
    }
}
