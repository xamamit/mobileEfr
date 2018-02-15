using Acs.Services.RegistrationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Acs.Services.Tests.RegistrationServices
{
    [TestClass]
    public class RegistrerDeviceServiceTests
    {
        private MockRepository mockRepository;



        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange


            // Act
            RegistrerDeviceService service = this.CreateService();


            // Assert

        }

        private RegistrerDeviceService CreateService()
        {
            return new RegistrerDeviceService();
        }
    }
}
