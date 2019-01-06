namespace atmps.domain.appServices
{
    /// <summary>
    /// Query.
    /// </summary>
    public interface IQuery<TResult>
    {
    }

    /// <summary>
    /// Query handler.
    /// </summary>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
