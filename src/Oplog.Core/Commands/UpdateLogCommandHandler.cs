using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Commands
{
    public class UpdateLogCommandHandler : ICommandHandler<UpdateLogCommand>
    {
        private readonly ILogsRepository _logsRepository;

        public UpdateLogCommandHandler(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }
        public async Task Handle(UpdateLogCommand command)
        {
            var log = new Log
            {
                LogTypeId = command.LogType,
                OperationAreaId = command.OperationsAreaId,
                Author = command.Author,
                Unit = command.Unit,
                Subtype = command.SubType,
                Text = command.Comment,
                EffectiveTime = command.EffectiveTime,
                UpdatedDate = DateTime.Now,
                UpdatedBy = command.UpdatedBy,
                IsCritical = command.IsCritical
            };

            _logsRepository.Update(log);
            await _logsRepository.Save();
        }
    }
}
