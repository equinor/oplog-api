using Oplog.Core.Enums;

namespace Oplog.Core.Commands.LogTemplates
{
    public class UpdateLogTemplateResult
    {
        public string Message { get; set; }
        public ResultType ResultType { get; set; }
        public int LogTemplateId { get; set; }

        public UpdateLogTemplateResult NotFound()
        {
            ResultType = ResultType.NotFound;
            Message = "Log not found";
            return this;
        }
        public UpdateLogTemplateResult LogTemplateUpdated(int logTemplateId)
        {
            LogTemplateId = logTemplateId;
            ResultType = ResultType.Success;
            Message = "Log updated";
            return this;
        }
    }
}
