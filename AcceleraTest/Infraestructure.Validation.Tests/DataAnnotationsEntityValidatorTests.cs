using System.Linq;
using Infraestructure.Validation.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infraestructure.Validation.Tests
{
    [TestClass]
    public class DataAnnotationsEntityValidatorTests
    {
        [TestMethod]
        public void DataAnnotationsValidatorIsValidShouldBeReturnFalseWithAnInvalidEntity()
        {
            var invalidFoo = new Foo();

            var validator = DataAnnotationsEntityValidatorFactory.Create();
            Assert.IsFalse(validator.IsValid(invalidFoo));
        }

        [TestMethod]
        public void DataAnnotationsValidatorGetValidationMessagesShouldBeReturnAMessageWithAnInvalidEntity()
        {
            var invalidFoo = new Foo();

            var validator = DataAnnotationsEntityValidatorFactory.Create();
            Assert.IsTrue(validator.GetValidationMessages(invalidFoo).ToList().Any(x => x.Key == "Name"));
        }
    }
}
