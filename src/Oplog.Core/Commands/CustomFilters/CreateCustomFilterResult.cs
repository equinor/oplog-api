using Oplog.Core.Common;

namespace Oplog.Core.Commands.CustomFilters
{
    public class CreateCustomFilterResult
    {
        public string Message { get; set; }
        public string ResultType { get; set; }
        public CreateCustomFilterResult CustomFilterCreated()
        {
            ResultType = ResultTypeConstants.Success;
            Message = "Custom/global filter created!";
            return this;
        }

        public CreateCustomFilterResult GlobalFiltercCreatedNotAllowed()
        {
            ResultType = ResultTypeConstants.NotAllowed;
            Message = "Only admins can create global filters!";
            return this;
        }
    }
}
