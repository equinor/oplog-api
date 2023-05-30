using Oplog.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Commands
{
    public class DeleteUserDefinedFilterCommand : ICommand
    {
        public DeleteUserDefinedFilterCommand(int filterId)
        {
            FilterId = filterId;
        }
        public int FilterId { get; set; }
    }
}
