
namespace Oplog.Core.Commands
{
    public class CreateCustomFilterResult
    {
        public string Message { get; set; }
        public ResultType ResultType { get; set; }
        public CreateCustomFilterResult CustomFilterCreated()
        {
            ResultType = ResultType.Success;
            Message = "Custom/global filter created!";
            return this;
        }

        public CreateCustomFilterResult GlobalFiltercCreatedNotAllowed()
        {
            ResultType = ResultType.NotAllowed;
            Message = "Only admins can create global filters!";
            return this;
        }
    }
}
