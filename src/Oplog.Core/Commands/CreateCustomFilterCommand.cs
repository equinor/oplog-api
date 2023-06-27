using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands
{
    public class CreateCustomFilterCommand : ICommand
    {
        public CreateCustomFilterCommand(string name, string createdBy, bool? isGlobalFilter, string searchText, bool isAdmin, List<CreateCustomFilterItem> filterItems)
        {
            Name = name;
            CreatedBy = createdBy;
            IsGlobalFilter = isGlobalFilter;
            SearchText = searchText;
            IsAdmin = isAdmin;
            FilterItems = filterItems;
        }

        public string Name { get; set; }
        public bool? IsGlobalFilter { get; set; }
        public string SearchText { get; set; }
        public string CreatedBy { get; set; }
        public bool IsAdmin { get; set; }
        public List<CreateCustomFilterItem> FilterItems { get; set; }
       
    }
}
