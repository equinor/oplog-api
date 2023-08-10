using Oplog.Core.Enums;

namespace Oplog.Core.Commands.Logs
{
    public class UpdateLogResult
    {
        public string Message { get; set; }
        public ResultType ResultType { get; set; }

        public UpdateLogResult NotFound()
        {
            ResultType = ResultType.NotFound;
            Message = "Log not found";
            return this;
        }
        public UpdateLogResult LogUpdated()
        {
            ResultType = ResultType.Success;
            Message = "Log updated";
            return this;
        }
    }
}
