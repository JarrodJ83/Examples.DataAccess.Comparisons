namespace Queries
{
    /// <summary>
    /// Represents a the intent to run a query to be handled by an instance of <see cref="IQueryHandler{TQuery, TResult}"/>. 
    /// Iplementations should contain all the information the handler would need to execute the query
    /// </summary>
    /// <typeparam name="TResult">Type of result expected when query is executed by an instance of <see cref="IQueryHandler{TQuery, TResult}"/></typeparam>
    public abstract class Query<TResult>
    {
        
    } 
}