using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Contracts;

namespace Infraestructure.Validation.Tests.Fakes
{
    public class Foo : Entity
    {
        public string Name { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Name))
                validationResults.Add(new ValidationResult("O nome não pode ser vazio", new[] { "Name" }));

            return validationResults;
        }
    }
}
