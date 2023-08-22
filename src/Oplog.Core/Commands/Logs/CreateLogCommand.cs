﻿using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.Logs
{
    public class CreateLogCommand : ICommand
    {
        public int LogType { get; set; }
        public int SubType { get; set; }
        public string Comment { get; set; }
        public int OperationsAreaId { get; set; }
        public string Author { get; set; }
        public int Unit { get; set; }
        public DateTime EffectiveTime { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsCritical { get; set; }

        public CreateLogCommand(int logType, int subType, string comment, int operationsArea, string author, int unit, DateTime effectiveTime, string createdBy, bool? isCritical)
        {
            LogType = logType;
            SubType = subType;
            Comment = comment;
            OperationsAreaId = operationsArea;
            Author = author;
            Unit = unit;
            EffectiveTime = effectiveTime;
            CreatedBy = createdBy;
            IsCritical = isCritical;
        }
    }
}