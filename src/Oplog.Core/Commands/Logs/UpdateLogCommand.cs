using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands.Logs
{
    public class UpdateLogCommand : ICommand
    {
        public int Id { get; set; }
        public int LogType { get; set; }
        public int SubType { get; set; }
        public string Comment { get; set; }
        public int OperationsAreaId { get; set; }
        public string Author { get; set; }
        public int Unit { get; set; }
        public DateTime EffectiveTime { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsCritical { get; set; }

        public UpdateLogCommand(int id, int logType, int subType, string comment, int operationsArea, string author, int unit, DateTime effectiveTime, string updatedBy, bool? isCritical)
        {
            Id = id;
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
