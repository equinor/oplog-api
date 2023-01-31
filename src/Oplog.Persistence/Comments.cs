using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Persistence
{
    public class Comments
    {
        public int Id { get; set; }
        public int CommentOrLogType { get; set; }
        public int ParentId { get; set; }
        public int LastChangeUserId { get; set; }
        public DateTime LastChangeTime { get; set; }
        public int CreatedBy { get; set; }
        public string Author { get; set; }
        public int ScheduleItemState { get; set; }
        public int CommentOrLogTypeId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text { get; set; }
        public int OperationAreaId { get; set; }
        public DateTime EffectiveTime { get; set; }
        public int Cat2TypId { get; set; }
        public int Cat3TypId { get; set; }
        public int Cat4TypId { get; set; }
        public int Cat5TypId { get; set; }
    }
}
