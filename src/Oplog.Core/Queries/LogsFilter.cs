namespace Oplog.Core.Queries
{
    public class LogsFilter
    {
        public LogsFilter(int[] logTypeIds, int[] areaIds, int[] subTypeIds, int[] unitIds, string searchText, DateTime fromDate, DateTime toDate)
        {
            LogTypeIds = logTypeIds;
            AreaIds = areaIds;
            SubTypeIds = subTypeIds;
            UnitIds = unitIds;
            SearchText = searchText;
            FromDate = fromDate;
            ToDate = toDate;
        }
        public DateTime FromDate { get; set; }
        public string SearchText { get; set; }
        public DateTime ToDate { get; set; }
        public int[] LogTypeIds { get; set; }
        public int[] AreaIds { get; set; }
        public int[] SubTypeIds { get; set; }
        public int[] UnitIds { get; set; }
    }
}
