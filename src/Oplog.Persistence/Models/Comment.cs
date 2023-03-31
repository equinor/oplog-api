using System;

namespace Oplog.Persistence.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int? LogTypeId { get; set; }
        public int? ParentId { get; set; }
        public int? LastChangeUserId { get; set; }
        public DateTime? LastChangeDateTime { get; set; }
        public int? CreatedBy { get; set; }
        public string Author { get; set; }
        public int? ScheduleItemState { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string Text { get; set; }
        public int? OperationAreaId { get; set; }
        public DateTime? EffectiveTime { get; set; }
        public int? Unit { get; set; }
        public int? Subtype { get; set; }

        //public int? Cat2TypId { get; set; }
        //public int? Cat3TypId { get; set; }
        //public int? Cat4TypId { get; set; }
        //public int? Cat5TypId { get; set; }
    }
}
