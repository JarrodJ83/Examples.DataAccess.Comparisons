using System.Threading;
using System.Threading.Tasks;

namespace Queries
{
    /// <summary>
    /// Generic interface for handling <see cref="Query{TResult}"/>
    /// </summary>
    /// <typeparam name="TQuery"><see cref="IQueryDef{TResult}"/> that will be handled</typeparam>
    /// <typeparam name="TResult">Result of query that matches type of the <see cref="IQueryDef{TResult}"/></typeparam>
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQueryDef<TResult>
    {
        Task<TResult> FetchAsync(TQuery query, CancellationToken cancellationToken = default(CancellationToken));
    }
}