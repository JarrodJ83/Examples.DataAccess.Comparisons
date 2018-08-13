namespace Requests
{
    /// <summary>
    /// Represents and incoming request to the API which does not require a response (For example, a POST that returns Accepted) 
    /// that will be handled by an instance of <see cref="IRequestHandler{TRequest}"/>
    /// 
    /// Implementations should contain all the information the handler would need to handle the request
    /// </summary>
    public interface IRequest
    {

    }

    /// <summary>
    /// Represents and incoming request to the API which requires a response (For example, a GET that returns some data) 
    /// that will be handled by an instance of <see cref="IRequestHandler{TRequest, TResponse}"/>
    /// 
    /// Implementations should contain all the information the handler would need to handle the request
    /// </summary>
    /// <typeparam name="TResponse">Type of the response expected when an instance of <see cref="IRequestHandler{TRequest, TResponse}"/> 
    /// handles the request</typeparam>
    public interface IRequest<TResponse> : IRequest
    {
        
    } 
}