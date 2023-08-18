namespace Application.Web;
public interface IEndpoint<TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request);
}
