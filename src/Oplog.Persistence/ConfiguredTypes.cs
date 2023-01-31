using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence
{
    public class ConfiguredTypes
    {
        public int Id { get; set; }
        public int ConfiguredType { get; set; }
        public int LastChangeUserId { get; set; }
        public DateTime LastChangeTime { get; set; }
        public DateTime StartLife { get; set; }
        public DateTime EndLife { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int UomTypeId { get; set; }
        public int DefaultUomId { get; set; }
        public int CategoryId { get; set; }
        public int Duplicate { get; set; }

    }
}
