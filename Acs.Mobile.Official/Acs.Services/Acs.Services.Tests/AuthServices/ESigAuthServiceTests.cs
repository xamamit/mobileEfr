
using Acs.Services.AuthServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Acs.Services.Tests.AuthServices
{
    [TestClass]
    public class ESigAuthServiceTests
    {
        private MockRepository _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRepository = new MockRepository(MockBehavior.Strict);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._mockRepository.VerifyAll();
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange


            // Act
          ESigAuthService service = this.CreateService();


            // Assert

        }

        private ESigAuthService CreateService()
        {
            return new ESigAuthService();
        }
    }
}