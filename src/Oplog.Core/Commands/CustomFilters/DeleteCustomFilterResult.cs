using Oplog.Core.Enums;

namespace Oplog.Core.Commands.CustomFilters
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
            Message = "Only admins can delete global filtyer!";
            return this;
        }
    }
}
