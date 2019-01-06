using atmps.domain.appServices.Utilities;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Messages tests.
    /// </summary>
    public class MessagesTests : IClassFixture<MessagesFixture>
    {
        private ServiceProvider _serviceProvider;

        public MessagesTests(MessagesFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        /// <summary>
        /// Dispacts the should match correct type and handler.
        /// </summary>
        [Fact]
        public void Dispact_ShouldMatchCorrectTypeAndHandler()
        {
            var messages = new Messages(_serviceProvider);

            var result = messages.Dispatch(new TestCommand());

            result.GetType().Should().Be(typeof(Result));

            result.IsSuccess.Should().BeTrue();
        }
    }

    /// <summary>
    /// Messages fixture.
    /// </summary>
    public class MessagesFixture
    {
        public MessagesFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ICommand, TestCommand>();
            serviceCollection.AddTransient<ICommandHandler<TestCommand>, TestCommandHandler>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }

    /// <summary>
    /// Test command.
    /// </summary>
    public class TestCommand : ICommand { }


    /// <summary>
    /// Test command handler.
    /// </summary>
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public Result Handle(TestCommand command)
        {
            return Result.Ok();
        }
    }
}
