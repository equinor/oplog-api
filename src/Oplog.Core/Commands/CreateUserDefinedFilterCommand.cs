using Oplog.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Commands
{
    public class CreateUserDefinedFilterCommand : ICommand
    {
        public CreateUserDefinedFilterCommand(string name,string  createdBy, List<UserDefinedFilterItem> filterItems)
        {
            Name = name;
            CreatedBy = createdBy;
            FilterItems = filterItems;
        }

        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public List<UserDefinedFilterItem> FilterItems { get; set; }
    }
}
