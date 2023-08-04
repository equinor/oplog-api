using Oplog.Core.Enums;

namespace Oplog.Core.Commands.LogTemplates
{
    public class CreateLogTemplateResult
    {
        public string Message { get; set; }
        public ResultType ResultType { get; set; }

        public CreateLogTemplateResult LogTemplateCreated()
        {
            ResultType = ResultType.Success;
            Message = "Log template created";
            return this;
        }
    }
}
