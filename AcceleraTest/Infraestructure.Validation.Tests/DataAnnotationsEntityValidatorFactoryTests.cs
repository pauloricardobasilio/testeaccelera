using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infraestructure.Validation.Tests
{
    [TestClass]
    public class DataAnnotationsEntityValidatorFactoryTests
    {
        [TestMethod]
        public void CreateMethodShouldBeReturnAnInstanceOfDataAnnotationsEntityValidator()
        {
            var validator = DataAnnotationsEntityValidatorFactory.Create();
            Assert.IsNotNull(validator);
            Assert.IsInstanceOfType(validator, typeof(DataAnnotationsEntityValidator));
        }
    }
}
