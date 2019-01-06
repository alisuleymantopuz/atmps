using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Manufacturer query handler tests.
    /// </summary>
    public class ManufacturerQueryHandlerTests
    {
        Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        /// <summary>
        /// Handlers the should return manufacturer from config store.
        /// </summary>
        [Fact]
        public void Handler_ShouldReturnManufacturerFromConfigStore()
        {
            var expectedResult = "test";

            _configuration.Setup(x => x[ConfigurationKeys.Manufacturer]).Returns(expectedResult);

            var queryHandler = new ManufacturerQueryHandler(_configuration.Object);

            var result = queryHandler.Handle(new ManufacturerQuery());

            result.Should().BeSameAs(expectedResult);
        }
    }
}
