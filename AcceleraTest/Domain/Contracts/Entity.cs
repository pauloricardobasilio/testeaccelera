using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Contracts
{
    public abstract class Entity : IValidatableObject
    {
        public int Id { get; set; }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
