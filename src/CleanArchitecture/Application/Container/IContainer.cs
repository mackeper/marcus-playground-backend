namespace Application.Container;
public interface IContainer
{
    T Resolve<T>() where T : notnull;
}
