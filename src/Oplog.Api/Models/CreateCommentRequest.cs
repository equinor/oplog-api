using System;

namespace Oplog.Api.Models
{
    public class CreateCommentRequest
    {
        public int LogType { get; set; }
        public int SubType { get; set; }
        public string Comment { get; set; }
        public int OperationsAreaId { get; set; }
        public string Author { get; set; }
        public int Unit { get; set; }
        public DateTime EffectiveTime { get; set; }
    }
}
