using Oplog.Core.Commands;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oplog.Api.ValidationAttributes
{
    public class ValidateFilterItems : ValidationAttribute
    {
        public int NoOfFilters { get; set; }

        public override bool IsValid(object value)
        {
            var list = value as IList<CreateCustomFilterItem>;
            if (list != null)
            {
                if (list.Count < NoOfFilters)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
