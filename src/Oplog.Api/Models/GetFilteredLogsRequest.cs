using System;

namespace Oplog.Api.Models
{
    public class GetFilteredLogsRequest
    {
        public int[] LogTypeIds { get; set; }
        public int[] AreaIds { get; set; }
        public int[] SubTypeIds { get; set; }
        public int[] UnitIds { get; set; }
        public string SearchText { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
