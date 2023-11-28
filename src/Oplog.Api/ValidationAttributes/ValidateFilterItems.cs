using Oplog.Api.Models;
using Oplog.Core.Commands.CustomFilters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oplog.Api.ValidationAttributes
{
    public class ValidateFilterItems : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IList<CreateCustomFilterItem> list)
            {
                if (list.Count < 1)
                {
                    return new ValidationResult(ErrorMessage ?? "At least one filter item is required.");
                }
                // get the CreateCustomFilterRequest object from the validation context
                var request = (CreateCustomFilterRequest)validationContext.ObjectInstance;

                if (list.Count < 2 && string.IsNullOrWhiteSpace(request.SearchText))
                {
                    return new ValidationResult(ErrorMessage ?? "Search text should be specified when there is only one filter item.");
                }
            }
            return ValidationResult.Success;
        }
    }
}