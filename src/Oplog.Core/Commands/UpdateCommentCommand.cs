using Oplog.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Commands
{
    public class UpdateCommentCommand : ICommand
    {
        public int LogType { get; set; }
        public int SubType { get; set; }
        public string Comment { get; set; }
        public int OperationsAreaId { get; set; }
        public string Author { get; set; }
        public int Unit { get; set; }
        public DateTime EffectiveTime { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsCritical { get; set; }

        public UpdateCommentCommand(int logType, int subType, string comment, int operationsArea, string author, int unit, DateTime effectiveTime, string updatedBy, bool? isCritical)
        {
            LogType = logType;
            SubType = subType;
            Comment = comment;
            OperationsAreaId = operationsArea;
            Author = author;
            Unit = unit;
            EffectiveTime = effectiveTime;
            UpdatedBy = updatedBy;
            IsCritical = isCritical;
        }
    }
}
