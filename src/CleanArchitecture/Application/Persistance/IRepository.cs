using SharedKernel.DDD;
using SharedKernel.Result;

namespace Infrastructure.Persistance;
public interface IRepository<T>
{
    Task<Result<T[]>> GetAll();
    Task<Result<T>> Get(Guid id);
    Task<Result<T>> Create(T entity);
    Task<Result> Update(T entity);
    Task<Result> Delete(Guid id);
}
