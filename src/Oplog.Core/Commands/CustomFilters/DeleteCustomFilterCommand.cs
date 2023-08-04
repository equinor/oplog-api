using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.CustomFilters
{
    public class DeleteCustomFilterCommand : ICommand
    {
        public DeleteCustomFilterCommand(int filterId, bool isAdmin)
        {
            FilterId = filterId;
            IsAdmin = isAdmin;
        }
        public int FilterId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
