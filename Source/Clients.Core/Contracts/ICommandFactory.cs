namespace Clients.Core.Contracts
{
    public interface ICommandFactory
    {
        TCommand MakeCommand<TCommand>() where TCommand : ICommandBase;
    }
}
