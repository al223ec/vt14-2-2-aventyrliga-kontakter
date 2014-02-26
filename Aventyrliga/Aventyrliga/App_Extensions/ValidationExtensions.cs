using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aventyrliga
{
    public static class ValidationExtensions
    {
        public static bool Validate<Contact>(this Contact instance, out ICollection<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, new ValidationContext(instance), validationResults, true);
        }
    }
}