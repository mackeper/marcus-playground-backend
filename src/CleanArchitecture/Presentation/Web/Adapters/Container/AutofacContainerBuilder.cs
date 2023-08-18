using Autofac;

namespace Web.Adapters.Container;
internal class AutofacContainerBuilder : Application.Container.IContainerBuilder
{
    ContainerBuilder containerBuilder;

    public AutofacContainerBuilder()
    {
        containerBuilder = new ContainerBuilder();
    }

    public Application.Container.IContainer Build() => new AutofacContainer(containerBuilder.Build());

    public void RegisterAs<T>(Func<T> factory) where T : notnull => containerBuilder.Register(_ => factory()).As<T>();
    public void RegisterType<T>() where T : notnull => containerBuilder.RegisterType<T>();
    public void RegisterInstance<T>(T instance) where T : class => containerBuilder.RegisterInstance(instance);
}
