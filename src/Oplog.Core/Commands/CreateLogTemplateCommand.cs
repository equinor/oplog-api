using Oplog.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Commands
{
    public class CreateLogTemplateCommand : ICommand
    {
        public CreateLogTemplateCommand(string name, int? logTypeId, int? areaId, string text, string author, int? unit, int? subType, bool? isCritical, string createdBy)
        {
            Name = name;
            LogTypeId = logTypeId;
            OperationAreaId = areaId;
            Text = text;
            Author = author;
            Unit = unit;
            Subtype = subType;
            IsCritical = isCritical;
            CreatedBy = createdBy;
        }
        public string Name { get; set; }
        public int? LogTypeId { get; set; }
        public int? OperationAreaId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int? Unit { get; set; }
        public int? Subtype { get; set; }
        public bool? IsCritical { get; set; }
        public string CreatedBy { get; set; }
    }
}
