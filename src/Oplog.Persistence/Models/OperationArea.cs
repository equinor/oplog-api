using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence.Models
{
    public class OperationArea
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        //Note: use tag from the legacy database
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
