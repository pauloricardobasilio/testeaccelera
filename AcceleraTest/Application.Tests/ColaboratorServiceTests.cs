using System;
using System.Collections.Generic;
using System.Linq;
using Application.Base;
using Domain;
using Infraestructure.Data.Contracts;
using Infraestructure.Validation.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Application.Tests
{
    [TestClass]
    public class ColaboratorServiceTests
    {
        private IColaboratorService _colaboratorService;

        private List<Colaborator> _colaborators;

        [TestInitialize]
        public void Initialize()
        {
            var mockRepository = new Mock<IColaboratorRepository>();

            _colaborators = new List<Colaborator>
                                   {
                                       new Colaborator
                                           {
                                               Id = 1,
                                               Name = "Fake Colaborator",
                                               DateOfBirth = new DateTime(1990, 01, 01),
                                               Registry = "11",
                                               PhoneNumber = "(69) 9999-9999",
                                               Address = "Fake Street, 11",
                                               Estate = "FE",
                                               City = "Fake City"
                                           },
                                        new Colaborator
                                            {
                                                Id = 2,
                                                Name = "Fake Colaborator 2",
                                                DateOfBirth = new DateTime(1991, 01, 01),
                                                Registry = "22",
                                                PhoneNumber = "(69) 9999-9999",
                                                Address = "Fake Street, 22",
                                                Estate = "FE",
                                                City = "Fake City"
                                            }
                                   };

            var mockUnityOfWork = new Mock<IEFUnityOfWork>();

            mockUnityOfWork.Setup(x => x.Commit());
            mockUnityOfWork.Setup(x => x.Rollback());

            mockRepository.SetupGet(x => x.UnityOfWork).Returns(mockUnityOfWork.Object);

            mockRepository.Setup(x => x.GetAll()).Returns(_colaborators);

            mockRepository.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) => _colaborators.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(x => x.GetByFilter(It.IsAny<Func<Colaborator, bool>>())).Returns(
                (Func<Colaborator, bool> filter) => _colaborators.Where(filter));

            mockRepository.Setup(x => x.GetByRegistry(It.IsAny<string>())).Returns(
                (string registry) => _colaborators.FirstOrDefault(x => x.Registry.ToLower() == registry.ToLower()));

            mockRepository.Setup(x => x.Exists(It.IsAny<string>())).Returns(
                (string registry) => _colaborators.Any(x => x.Registry.ToLower() == registry));

            mockRepository.Setup(x => x.ExistsAnother(It.IsAny<string>(), It.IsAny<int>())).Returns(
                (string registry, int id) =>
                _colaborators.Any(x => x.Registry.ToLower() == registry.ToLower() && x.Id != id));

            mockRepository.Setup(x => x.Add(It.IsAny<Colaborator>())).Callback((Colaborator colaborator) => FakeAddColaborator(colaborator));
            mockRepository.Setup(x => x.Modify(It.IsAny<Colaborator>())).Callback(
                (Colaborator colaborator) => FakeUpdateColaborator(colaborator));

            _colaboratorService = new ColaboratorService(mockRepository.Object);
        }

        private void FakeUpdateColaborator(Colaborator colaborator)
        {
            foreach (var colaboratorPersisted in _colaborators.Where(colaborator1 => colaborator1.Id == colaborator.Id))
            {
                colaboratorPersisted.Name = colaborator.Name;
                colaboratorPersisted.Registry = colaborator.Registry;
                colaboratorPersisted.DateOfBirth = colaborator.DateOfBirth;
                colaboratorPersisted.PhoneNumber = colaborator.PhoneNumber;
                colaboratorPersisted.Address = colaborator.Address;
                colaboratorPersisted.Estate = colaborator.Estate;
                colaboratorPersisted.City = colaborator.City;
            }
        }

        private void FakeAddColaborator(Colaborator colaborator)
        {
            var id = _colaborators.Max(x => x.Id) + 1;
            colaborator.Id = id;
            _colaborators.Add(colaborator);
        }

        [TestMethod]
        public void ColaboratorServiceFindColaboratorsShouldBeReturnAllColaborators()
        {
            var colaborators = _colaboratorService.FindColaborators();
            Assert.IsNotNull(colaborators);
            Assert.AreEqual(2, colaborators.Count());
        }

        [TestMethod]
        public void ColaboratorServiceFindColaboratorByIdShouldBeReturnAColaboratorMatchesId()
        {
            var colaborator = _colaboratorService.FindColaboratorById(1);

            Assert.IsNotNull(colaborator);
            Assert.AreEqual(1, colaborator.Id);
        }

        [TestMethod]
        public void ColaboratorServiceFindColaboratorsShouldBeReturnsAllColaboratorsMatchingFilter()
        {
            var colaborators = _colaboratorService.FindColaborators(x => x.Estate == "FE");

            Assert.IsNotNull(colaborators);
            Assert.AreEqual(2, colaborators.Count);
        }

        [TestMethod]
        public void ShouldBeFindAColaboratorByRegistry()
        {
            var colaborator = _colaboratorService.FindColaboratorByRegistry("11");
            Assert.IsNotNull(colaborator);
            Assert.AreEqual("11", colaborator.Registry);
        }

        [TestMethod]
        public void ShouldBeAddAColaboratorSuccess()
        {
            var colaborator = new Colaborator
                                              {
                                                  Address = "Rua dos Testes",
                                                  City = "Ariquemes",
                                                  DateOfBirth = new DateTime(2012, 06, 02),
                                                  Estate = "RO",
                                                  Registry = "aabbcc1232",
                                                  Name = "Jose Pereira",
                                                  PhoneNumber = "(99) 9999-9999"
                                              };

            var errors = new List<Error>();

            try
            {
                _colaboratorService.AddColaborator(colaborator);
            }
            catch (ApplicationValidationErrorsException exception)
            {
                errors.AddRange(exception.ValidationErrors);
            }

            Assert.IsFalse(errors.Any());
            Assert.AreEqual(3, colaborator.Id);
        }

        [TestMethod]
        public void ShouldBeTryAddAColaboratorWithADuplicateRegistry()
        {
            var colaborator = new Colaborator
                                  {
                                      Name = "Colaborador Invalido",
                                      Registry = "11",
                                      DateOfBirth = new DateTime(2000, 11, 02),
                                      PhoneNumber = "(69) 9949-0353",
                                      Address = "Rua Perdizes",
                                      Estate = "RO",
                                      City = "Ariquemes"
                                  };

            var errors = new List<Error>();

            try
            {
                _colaboratorService.AddColaborator(colaborator);
            }
            catch (ApplicationValidationErrorsException exception)
            {
                errors.AddRange(exception.ValidationErrors);
            }
            Assert.AreEqual("Registry", errors.FirstOrDefault().Key);
        }

        [TestMethod]
        public void ShouldBeTryAddAColaboratorWithAEmptyRegistry()
        {
            var colaborator = new Colaborator
            {
                Name = "Colaborador Invalido",
                DateOfBirth = new DateTime(2000, 11, 02),
                PhoneNumber = "(69) 9949-0353",
                Address = "Rua Perdizes",
                Estate = "RO",
                City = "Ariquemes"
            };

            var errors = new List<Error>();

            try
            {
                _colaboratorService.AddColaborator(colaborator);
            }
            catch (ApplicationValidationErrorsException exception)
            {
                errors.AddRange(exception.ValidationErrors);
            }
            Assert.AreEqual("Registry", errors.FirstOrDefault().Key);
        }

        [TestMethod]
        public void ShouldBeUpdateAColaborator()
        {
            var toUpdate = new Colaborator
                               {
                                   Id = 1,
                                   Address = "Rua Juriti, 1849",
                                   City = "Ariquemes",
                                   DateOfBirth = new DateTime(1990, 06, 02),
                                   Estate = "BA",
                                   Name = "Pedro Silva",
                                   PhoneNumber = "(69) 9949-0353",
                                   Registry = "aabbcc1222"
                               };

            var errors = new List<Error>();

            try
            {
                _colaboratorService.UpdateColaborator(toUpdate);
            }
            catch (ApplicationValidationErrorsException exception)
            {
                errors.AddRange(exception.ValidationErrors);
            }

            var expectedColaborator = _colaborators.FirstOrDefault(x => x.Id == 1);

            Assert.IsFalse(errors.Any());
            Assert.IsNotNull(expectedColaborator);
            Assert.AreEqual(toUpdate.Registry, expectedColaborator.Registry);
        }
    }
}
