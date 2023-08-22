﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oplog.Core.Queries.ConfiguredTypes
{
    public class GetUnitsByAreaName
    {
        public GetUnitsByAreaName(int id, string name, string description, int? categoryId)
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