using Oplog.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence.Repositories
{
    public interface IOperationsAreasRepository
    {
        Task<List<OperationArea>> GetAllAreas();
    }
}
