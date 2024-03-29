﻿using Oplog.Api.ValidationAttributes;
using Oplog.Core.Commands.CustomFilters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oplog.Api.Models
{
    public class CreateCustomFilterRequest
    {
        [Required]
        public string Name { get; set; }
        public bool? IsGlobalFilter { get; set; }
        public string SearchText { get; set; }
        [Required]
        [ValidateFilterItems]
        public List<CreateCustomFilterItem> FilterItems { get; set; }
    }
}
