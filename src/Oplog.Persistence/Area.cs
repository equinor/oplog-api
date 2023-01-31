using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence
{
    public class Area
    {
        public int Ind2User { get; set; }
        public DateTime Time { get; set; }
        public int Dbindex { get; set; }
        public string Tag { get; set; }
        public string Descript { get; set; }
        public int ObjType { get; set; }

    }
}
