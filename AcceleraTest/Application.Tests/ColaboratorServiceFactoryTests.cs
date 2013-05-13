using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Tests
{
    [TestClass]
    public class ColaboratorServiceFactoryTests
    {
        [TestMethod]
        public void ColaboratorServiceFactoryCreateMethodShouldBeReturnAnInstanceOfColaboratorService()
        {
            var colaboratorService = ColaboratorServiceFactory.Create();
            Assert.IsNotNull(colaboratorService);
            Assert.IsInstanceOfType(colaboratorService, typeof(ColaboratorService));
        }
    }
}
