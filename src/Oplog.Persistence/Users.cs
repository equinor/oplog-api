using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence
{
    public class Users
    {
        public int Ind2User { get; set; }
        public DateTime Time { get; set; }
        public int Dbindex { get; set; }
        public string Tag { get; set; }
        public string Descript { get; set; }
        public int Password { get; set; }
        public string Domain { get; set; }
        public string SidData { get; set; } //DB contains no data for this column
 
    }
}
