using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Serial number query handler tests.
    /// </summary>
    public class SerialNumberQueryHandlerTests
    {
        Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        /// <summary>
        /// Handlers the should return serial number from config store.
        /// </summary>
        [Fact]
        public void Handler_ShouldReturnSerialNumberFromConfigStore()
        {
            var expectedResult = "test";

            _configuration.Setup(x => x[ConfigurationKeys.SerialNumber]).Returns(expectedResult);

            var queryHandler = new SerialNumberQueryHandler(_configuration.Object);

            var result = queryHandler.Handle(new SerialNumberQuery());

            result.Should().BeSameAs(expectedResult);
        }
    }
}
