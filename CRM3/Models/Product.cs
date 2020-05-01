using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRM3.Models
{
    public class Product : IValidatableObject
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<CustomerAccountProduct> CustomerAccountProducts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.Length > 30)
            {
                yield return new ValidationResult(
                    "The product name is too long",
                    new[] { nameof(Name)});
            }
        }
    }
}
