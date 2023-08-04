using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oplog.Core.Enums;

namespace Oplog.Core.Commands.LogTemplates
{
    public class DeleteLogTemplateResult
    {
        public string Message { get; private set; }
        public ResultType ResultType { get; private set; }

        public DeleteLogTemplateResult LogtemplateNotFound()
        {
            ResultType = ResultType.NotFound;
            Message = "Log template not found!";
            return this;
        }

        public DeleteLogTemplateResult LogtemplateDeleted()
        {
            ResultType = ResultType.Success;
            Message = "log template deleted!";
            return this;
        }
    }
}
