﻿namespace Clients.Core.Contracts
{
    /// <summary>
    /// Represents an async query.
    /// </summary>
    /// <typeparam name="TQuery">Query options.</typeparam>
    /// <typeparam name="TResult">Query result.</typeparam>
    public interface IQuery<TQuery, TResult> : IQueryBase
        where TQuery : class
        where TResult : class
    {
        /// <summary>
        /// Execute the query.
        /// </summary>
        /// <param name="query">Nullable parameter.</param>
        /// <returns><typeparamref name="TResult"/></returns>
        TResult Execute(TQuery query);
    }

    /// <summary>
    /// Represent a query object
    /// </summary>
    public interface IQueryBase { }
}
