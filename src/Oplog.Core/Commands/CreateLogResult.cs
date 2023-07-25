namespace Oplog.Core.Commands
{
    public class CreateLogResult
    {
        public string Message { get; set; }
        public ResultType ResultType { get; set; }

        public CreateLogResult LogCreated()
        {
            ResultType = ResultType.Success;
            Message = "New log created";
            return this;
        }
    }
}
