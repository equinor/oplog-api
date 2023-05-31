namespace Oplog.Api.Models
{
    public class CreateLogTemplateRequest
    {
        public string Name { get; set; }
        public int? LogTypeId { get; set; }
        public int? OperationAreaId { get; set; }
        public string Text { get; set; }
        public string Authour { get; set; }
        public int? Unit { get; set; }
        public int? Subtype { get; set; }
        public bool? IsCritical { get; set; }
    }
}
