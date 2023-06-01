using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands
{
    public class CreateCustomFilterCommand : ICommand
    {
        public CreateCustomFilterCommand(string name, string createdBy, bool? isGlobalFilter, List<CreateCustomFilterItem> filterItems)
        {
            Name = name;
            CreatedBy = createdBy;
            IsGlobalFilter = isGlobalFilter;
            FilterItems = filterItems;
        }

        public string Name { get; set; }
        public bool? IsGlobalFilter { get; set; }
        public string CreatedBy { get; set; }
        public List<CreateCustomFilterItem> FilterItems { get; set; }
    }
}
