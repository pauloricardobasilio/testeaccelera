using System;
using System.Collections.Generic;
using Domain;
using Domain.Contracts;

namespace Infraestructure.Validation.Base
{
    public struct Error
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public interface IEntityValidator
    {
        bool IsValid<T>(T entity) where T : Entity;

        IList<Error> GetValidationMessages<T>(T entity) where T : Entity;
    }
}
