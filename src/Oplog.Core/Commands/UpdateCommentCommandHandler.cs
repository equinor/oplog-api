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
    public class UpdateCommentCommandHandler : ICommandHandler<UpdateCommentCommand>
    {
        private readonly ICommentsRepository _commentsRepository;

        public UpdateCommentCommandHandler(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }
        public async Task Handle(UpdateCommentCommand command)
        {
            var comment = new Comment
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

            _commentsRepository.Update(comment);
            await _commentsRepository.Save();
        }
    }
}
