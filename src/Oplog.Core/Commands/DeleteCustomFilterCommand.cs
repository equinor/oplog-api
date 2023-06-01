using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands
{
    public class DeleteCustomFilterCommand : ICommand
    {
        public DeleteCustomFilterCommand(int filterId)
        {
            FilterId = filterId;
        }
        public int FilterId { get; set; }
    }
}
