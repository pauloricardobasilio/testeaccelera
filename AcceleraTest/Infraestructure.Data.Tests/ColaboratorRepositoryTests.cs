using System;
using System.Data.Entity;
using System.Linq;
using Domain;
using Infraestructure.Data.Repositories;
using Infraestructure.Data.Tests.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infraestructure.Data.Tests
{
    [TestClass]
    public class ColaboratorRepositoryTests
    {
        private ColaboratorRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            var initializer = new UnityOfWorkInitializer();
            var unityOfWork = new UnityOfWork("TestDb");
            initializer.InitializeDatabase(unityOfWork);
            _repository = new ColaboratorRepository(unityOfWork);
        }

        [TestMethod]
        public void ColaboratorRepositoryGetShouldBeReturnAColaboratorById()
        {
            var colaborator = _repository.Get(1);
            Assert.IsNotNull(colaborator);
            Assert.IsTrue(colaborator.Id == 1);
        }

        [TestMethod]
        public void ColaboratorRepositoryGetShouldBeReturnNullIfIdIsInvalid()
        {
            var colaborator = _repository.Get(0);
            Assert.IsNull(colaborator);
        }

        [TestMethod]
        public void ColaboratorRepositoryGetByFilterShouldBeReturnColaboratorsWithSatisfiedCondition()
        {
            const string filter = "FE";

            var colaborators = _repository.GetByFilter(colaborator => colaborator.Estate == filter).ToList();

            Assert.IsNotNull(colaborators);
            Assert.IsTrue(colaborators.All(x => x.Estate == filter));
        }

        [TestMethod]
        public void ColaboratorRepositoryShouldBeAddColaboratorAndSave()
        {
            var colaborator = new Colaborator
            {
                Name = "Fake Colaborator New",
                DateOfBirth = new DateTime(2000, 01, 01),
                Registry = "66",
                PhoneNumber = "(69) 9944-4444",
                Address = "Fake Street, 66",
                Estate = "FE",
                City = "Fake City"
            };

            _repository.Add(colaborator);
            _repository.UnityOfWork.Commit();

            Assert.IsTrue(colaborator.Id > 0);
        }

        [TestMethod]
        public void ColaboratorRepositoryExistsShouldBeReturnTrueWithAValidRegistry()
        {
            const string registryToVerify = "11";
            Assert.IsTrue(_repository.Exists(registryToVerify));
        }

        [TestMethod]
        public void ColaboratorRepositoryExistsShouldBeReturnFalseWithAnInvalidRegistry()
        {
            const string registryToVerify = "00";
            Assert.IsFalse(_repository.Exists(registryToVerify));
        }

        [TestMethod]
        public void ColaboratorRepositoryExistsAnotherShouldBeReturnFalse()
        {
            Assert.IsFalse(_repository.ExistsAnother("11", 1));
        }

        [TestMethod]
        public void ColaboratorRepositoryShouldBeRemoveColaborator()
        {
            var colaboratorToRemove = _repository.Get(1);

            _repository.Remove(colaboratorToRemove);
            _repository.UnityOfWork.Commit();

            Assert.IsFalse(_repository.Exists(colaboratorToRemove.Registry));
        }

        [TestMethod]
        public void ColaboratorRepositoryGetByRegistryShouldBeReturnAColaborator()
        {
            const string validRegistry = "11";
            var colaborator = _repository.GetByRegistry(validRegistry);

            Assert.IsNotNull(colaborator);
            Assert.AreEqual(validRegistry, colaborator.Registry);
        }

        [TestMethod]
        public void ColaboratorRepositoryGetByRegistryShouldBeReturnNull()
        {
            const string invalidRegistry = "00";
            var colaborator = _repository.GetByRegistry(invalidRegistry);
            Assert.IsNull(colaborator);
        }

        [TestCleanup]
        public void Clean()
        {
            Database.Delete("TestDb");
        }
    }
}
