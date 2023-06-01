using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands
{
    public class DeleteLogTemplateCommand : ICommand
    {
        public DeleteLogTemplateCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
