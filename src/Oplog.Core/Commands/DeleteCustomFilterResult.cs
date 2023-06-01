namespace Oplog.Core.Commands
{
    public class DeleteCustomFilterResult
    {
        public string Message { get; set; }
        public ResultType ResultType { get; set; }

        public DeleteCustomFilterResult CustomFilterNotFound()
        {
            ResultType = ResultType.NotFound;
            Message = "Custom filter not found!";
            return this;
        }

        public DeleteCustomFilterResult CustomFilterDeleted()
        {
            ResultType = ResultType.Success;
            Message = "Custom filter deleted!";
            return this;
        }

        public DeleteCustomFilterResult GlobalFilterDeleteNotAllowed()
        {
            ResultType = ResultType.NotAllowed;
            Message = "Cannot delete global filters!";
            return this;
        }
    }
}
