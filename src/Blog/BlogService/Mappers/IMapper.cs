using BlogService.Domain;
using BlogService.DTOs;

namespace BlogService.Mappers;

internal interface IMapper<TEntity, TDto>
    where TEntity : IEntity
    where TDto : IDto
{
    public TEntity Map(TDto dto);
    public TDto Map(TEntity entity);
}
