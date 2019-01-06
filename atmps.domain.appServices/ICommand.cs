using CSharpFunctionalExtensions;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Command.
    /// </summary>
    public interface ICommand
    {
    }

    /// <summary>
    /// Command handler.
    /// </summary>
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }
}
