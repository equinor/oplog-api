using Oplog.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Commands
{
    public class DeleteLogTemplateCommand : ICommand
    {
        public DeleteLogTemplateCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
