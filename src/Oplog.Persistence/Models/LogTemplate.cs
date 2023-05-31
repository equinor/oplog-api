using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence.Models
{
    public class LogTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LogTypeId { get; set; }
        public int? OperationAreaId { get; set; }
        public string Text { get; set; }    
        public string Author { get; set; }
        public int? Unit { get; set; }
        public int? Subtype { get; set; }
        public bool? IsCritical { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
