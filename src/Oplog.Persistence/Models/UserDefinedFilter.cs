using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence.Models
{
    public class UserDefinedFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserDefinedFilterItem> UserDefinedFilterItems { get; set; }
        public string CreatedBy { get; set; }
    }
}
