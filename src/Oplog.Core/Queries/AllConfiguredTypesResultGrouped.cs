using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries
{
    public class AllConfiguredTypesResultGrouped
    {
        public AllConfiguredTypesResultGrouped()
        {
            Types = new List<ConfiguredTypeResult>();
            SubTypes = new List<ConfiguredTypeResult>();
            Units = new List<ConfiguredTypeResult>();
        }
        public List<ConfiguredTypeResult> Types { get; set; }
        public List<ConfiguredTypeResult> SubTypes { get; set; }
        public List<ConfiguredTypeResult> Units { get; set; }
    }

    public class ConfiguredTypeResult
    {
        public ConfiguredTypeResult(int id, string name, string description, int? categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            CategoryId = categoryId;

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
