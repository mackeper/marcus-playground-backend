namespace Application.Container;
public interface IContainerBuilder
{
    void RegisterType<T>() where T : notnull;
    void RegisterInstance<T>(T instance) where T : class;
    IContainer Build();

}
