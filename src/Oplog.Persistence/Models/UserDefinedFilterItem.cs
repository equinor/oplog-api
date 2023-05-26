namespace Oplog.Persistence.Models
{
    public class UserDefinedFilterItem
    {
        public int Id { get; set; }
        public int UserDefinedFilterId { get; set; }
        public int FilterId { get; set; }
        public int? CategoryId { get; set; }
        public UserDefinedFilter UserDefinedFilter { get; set; }
    }
}
