using Oplog.Core.Commands;
using System.Collections.Generic;

namespace Oplog.Api.Models
{
    public class CreateCustomFilterRequest
    {
        public string Name { get; set; }
        public bool? IsGlobalFilter { get; set; }
        public string SearchText { get; set; }
        public List<CreateCustomFilterItem> FilterItems { get; set; }
    }
}
