namespace Clients.Core.Contracts
{
    public interface ICommandBase { }

    public interface ICommand : ICommandBase
    {
        void Execute();
    }

    public interface ICommand<TParam> : ICommandBase
    {
        void Execute(TParam param);
    }
}
