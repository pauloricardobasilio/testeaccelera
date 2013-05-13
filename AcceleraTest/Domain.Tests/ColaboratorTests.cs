using System;
using System.Linq;
using Infraestructure.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    [TestClass]
    public class ColaboratorTests
    {
        [TestMethod]
        public void ColaboratorWithNameNullOrEmptyShouldBeInvalid()
        {
            var invalidColaborator = new Colaborator
                                         {
                                             Address = "Fake Address, 11",
                                             City = "Fake City",
                                             Estate = "FE",
                                             PhoneNumber = "(69) 9999-9999",
                                             Registry = "11",
                                             DateOfBirth = DateTime.Now
                                         };

            var validator = DataAnnotationsEntityValidatorFactory.Create();

            Assert.IsFalse(validator.IsValid(invalidColaborator));
        }

        [TestMethod]
        public void ColaboratorWithRegistryNullOrEmptyShouldBeInvalid()
        {
            var invalidColaborator = new Colaborator
            {
                Address = "Fake Address, 11",
                City = "Fake City",
                Estate = "FE",
                PhoneNumber = "(69) 9999-9999",
                DateOfBirth = DateTime.Now,
                Name = "Fake Name"
            };

            var validator = DataAnnotationsEntityValidatorFactory.Create();

            Assert.IsFalse(validator.IsValid(invalidColaborator));
        }

        [TestMethod]
        public void ColaboratorWithEstateNullOrEmptyShouldBeInvalid()
        {
            var invalidColaborator = new Colaborator
            {
                Address = "Fake Address, 11",
                City = "Fake City",
                PhoneNumber = "(69) 9999-9999",
                DateOfBirth = DateTime.Now,
                Name = "Fake Name",
                Registry = "11"
            };

            var validator = DataAnnotationsEntityValidatorFactory.Create();

            Assert.IsFalse(validator.IsValid(invalidColaborator));
        }

        [TestMethod]
        public void ColaboratorWithCityNullOrEmptyShouldBeInvalid()
        {
            var invalidColaborator = new Colaborator
            {
                Address = "Fake Address, 11",
                Estate = "FE",
                PhoneNumber = "(69) 9999-9999",
                DateOfBirth = DateTime.Now,
                Name = "Fake Name",
                Registry = "11"
            };

            var validator = DataAnnotationsEntityValidatorFactory.Create();

            Assert.IsFalse(validator.IsValid(invalidColaborator));
        }

        [TestMethod]
        public void ColaboratorWithAllRequirementsShouldBeValid()
        {
            var validColaborator = new Colaborator
                                       {
                                           Name = "Valid Colaborator",
                                           Registry = "10",
                                           DateOfBirth = DateTime.Now,
                                           PhoneNumber = "(69) 9999-9999",
                                           Address = "Valid colaborator address",
                                           City = "Valid City",
                                           Estate = "VE"
                                       };

            var validator = DataAnnotationsEntityValidatorFactory.Create();
            Assert.IsTrue(validator.IsValid(validColaborator));
        }
    }
}
