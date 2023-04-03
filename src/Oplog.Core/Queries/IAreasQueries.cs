using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries
{
    public interface IAreasQueries
    {
        Task<List<GetAllAreasResult>> GetAllAreas();
    }
}
