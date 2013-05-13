using System;
using System.Collections.Generic;
using System.Linq;
using Application.Base;
using Domain;
using Infraestructure.Validation;
using Infraestructure.Validation.Base;

namespace Application
{
    public class ColaboratorService : IColaboratorService
    {
        readonly IColaboratorRepository _colaboratorRepository;

        public ColaboratorService(IColaboratorRepository colaboratorRepository)
        {
            if (colaboratorRepository == null)
                throw new ArgumentException("colaboratorRepository");

            _colaboratorRepository = colaboratorRepository;
        }

        public void AddColaborator(Colaborator colaborator)
        {
            if (colaborator == null)
                throw new ArgumentException("invalid_colaborator");

            var validator = DataAnnotationsEntityValidatorFactory.Create();
            if (!validator.IsValid(colaborator))
                throw new ApplicationValidationErrorsException(validator.GetValidationMessages(colaborator));

            if (_colaboratorRepository.Exists(colaborator.Registry))
                throw new ApplicationValidationErrorsException(new Error { Key = "Registry", Value = "Já existe um colaborador com essa matricula" });

            _colaboratorRepository.Add(colaborator);
            _colaboratorRepository.UnityOfWork.Commit();
        }

        public void UpdateColaborator(Colaborator colaborator)
        {
            if (colaborator == null || colaborator.Id <= 0)
                throw new ArgumentException("invalid_colaborator");

            var validator = DataAnnotationsEntityValidatorFactory.Create();
            if (!validator.IsValid(colaborator))
                throw new ApplicationValidationErrorsException(validator.GetValidationMessages(colaborator));

            if (_colaboratorRepository.ExistsAnother(colaborator.Registry, colaborator.Id))
                throw new ApplicationValidationErrorsException(new Error { Key = "Registry", Value = "Já existe um colaborador com essa matricula" });

            _colaboratorRepository.Modify(colaborator);
            _colaboratorRepository.UnityOfWork.Commit();
        }

        public List<Colaborator> FindColaborators()
        {
            return _colaboratorRepository.GetAll().ToList();
        }

        public List<Colaborator> FindColaborators(Func<Colaborator, bool> filter)
        {
            return _colaboratorRepository.GetByFilter(filter).ToList();
        }

        public Colaborator FindColaboratorById(int id)
        {
            return _colaboratorRepository.Get(id);
        }

        public Colaborator FindColaboratorByRegistry(string registry)
        {
            return _colaboratorRepository.GetByRegistry(registry);
        }

        public void RemoveColaborator(int id)
        {
            var colaborator = _colaboratorRepository.Get(id);
            _colaboratorRepository.Remove(colaborator);
            _colaboratorRepository.UnityOfWork.Commit();
        }
    }
}
