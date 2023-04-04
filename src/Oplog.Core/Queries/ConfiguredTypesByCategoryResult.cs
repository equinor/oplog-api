using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries
{
    public class ConfiguredTypesByCategoryResult
    {
        public ConfiguredTypesByCategoryResult(int id, int configuredTypeId, string name, string description, int? categoryId)
        {
            Id = id;
            ConfiguredTypeId = configuredTypeId;
            Name = name;
            Description = description;
            CategoryId = categoryId;

        }
        public int Id { get; set; }
        public int ConfiguredTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
