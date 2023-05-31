using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries
{
    public class GetUserDefinedFiltersByCreatedUserResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserDefinedFilterItemsResult> Filters { get; set; }
    }

    public class UserDefinedFilterItemsResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
    }
}
