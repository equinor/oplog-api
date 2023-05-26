using Oplog.Core.Commands;
using System.Collections.Generic;

namespace Oplog.Api.Models
{
    public class CreateUserDefinedFilterRequest
    {
        public string Name { get; set; }
        public List<UserDefinedFilterItem> FilterItems { get; set; }
    }
}
