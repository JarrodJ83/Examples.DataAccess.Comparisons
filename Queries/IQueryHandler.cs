using System.Threading;
using System.Threading.Tasks;

namespace Queries
{
    /// <summary>
    /// Generic interface for handling <see cref="Query{TResult}"/>
    /// </summary>
    /// <typeparam name="TQuery"><see cref="Query{TResult}"/> that will be handled</typeparam>
    /// <typeparam name="TResult">Result of query that matches type of the <see cref="Query{TResult}"/></typeparam>
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQueryDef<TResult> where TResult : class
    {
        Task<TResult> FetchAsync(TQuery Query, CancellationToken cancellationToken = default(CancellationToken));
    }
}