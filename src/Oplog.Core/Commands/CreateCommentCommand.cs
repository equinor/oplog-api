using Oplog.Core.Infrastructure;

namespace Oplog.Core.Commands
{
    public class CreateCommentCommand : ICommand
    {
        public int LogType { get; set; }
        public int SubType { get; set; }
        public string Comment { get; set; }
        public int OperationsAreaId { get; set; }
        public string Author { get; set; }
        public int Unit { get; set; }
        public DateTime EffectiveTime { get; set; }

        public CreateCommentCommand(int logType, int subType, string comment, int operationsArea, string author, int unit, DateTime effectiveTime)
        {
            LogType = logType;
            SubType = subType;
            Comment = comment;
            OperationsAreaId = operationsArea;
            Author = author;
            Unit = unit;
            EffectiveTime = effectiveTime;
        }
    }
}
