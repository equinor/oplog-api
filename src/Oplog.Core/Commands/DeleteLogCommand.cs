using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands
{
    public class DeleteLogCommand : ICommand
    {
        public DeleteLogCommand(IEnumerable<int> ids)
        {
            Ids = ids.ToList();
        }

        public List<int> Ids { get; set; }
    }

}
