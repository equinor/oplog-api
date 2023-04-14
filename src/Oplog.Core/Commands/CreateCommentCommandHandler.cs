using Oplog.Core.Infrastructure;
using Oplog.Persistence.Models;
using Oplog.Persistence.Repositories;

namespace Oplog.Core.Commands
{
    public class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand>
    {
        private readonly ICommentsRepository _commentsRepository;

        public CreateCommentCommandHandler(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }
        public async Task Handle(CreateCommentCommand command)
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
                CreatedBy = command.CreatedBy,
                CreatedDate = DateTime.Now,
                IsCritical = command.IsCritical
            };

            await _commentsRepository.Insert(comment);
            await _commentsRepository.Save();
        }
    }
}
