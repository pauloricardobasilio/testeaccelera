using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Contracts;
using Infraestructure.Validation.Base;

namespace Infraestructure.Validation
{
    public class DataAnnotationsEntityValidator : IEntityValidator
    {
        public bool IsValid<T>(T entity) where T : Entity 
        {
            if (entity == null)
                return false;

            var errors = new List<Error>();

            GetObjectValidatableErrors(entity, errors);
            GetValidationAttributeErrors(entity, errors);

            return !errors.Any();
        }

        public IList<Error> GetValidationMessages<T>(T entity) where T : Entity
        {
            if (entity == null) return null;

            var errors = new List<Error>();

            GetObjectValidatableErrors(entity, errors);
            GetValidationAttributeErrors(entity, errors);

            return errors;
        }

        private static void GetObjectValidatableErrors<T>(T entity, List<Error> errors) where T : Entity
        {
            var context = new ValidationContext(entity, null, null);

            var results = ((IValidatableObject)entity).Validate(context).ToList();

            foreach (var validationResult in results)
            {
                errors.Add(new Error { Key = validationResult.MemberNames.FirstOrDefault(), Value = validationResult.ErrorMessage });
            }
        }

        private static void GetValidationAttributeErrors<T>(T entity, List<Error> errors) where T : Entity
        {
            var result = from property in TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>()
                         from attribute in property.Attributes.OfType<ValidationAttribute>()
                         where !attribute.IsValid(property.GetValue(entity))
                         select attribute.FormatErrorMessage(string.Empty);

            if (result.Any())
                errors.AddRange(result.Select(error => new Error() { Key = "generic_error", Value = error }));
        }
    }
}
