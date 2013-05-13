using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Contracts;

namespace Domain
{
    public class Colaborator : Entity
    {
        public Colaborator()
        {
            DateOfBirth = DateTime.Now;
        }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Registry { get; set; }

        public string Address { get; set; }

        public string Estate { get; set; }

        public string City { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Name))
                validationResults.Add(new ValidationResult("O nome não pode ser vazio", new[] { "Name" }));
            if (string.IsNullOrEmpty(Registry))
                validationResults.Add(new ValidationResult("A matricula do colaborador é obrigatória", new[] { "Registry" }));
            if (string.IsNullOrEmpty(Estate))
                validationResults.Add(new ValidationResult("O Estado é obrigatório", new[] { "Estate" }));
            if (string.IsNullOrEmpty(City))
                validationResults.Add(new ValidationResult("A Cidade é obrigatória", new[] { "City" }));

            return validationResults;
        }
    }
}
