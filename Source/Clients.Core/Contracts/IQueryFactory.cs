namespace Clients.Core.Contracts
{
    public interface IQueryFactory
    {
        TQuery MakeQuery<TQuery>() where TQuery : IQueryBase;
    }
}
