using Autofac;
using IContainer = Application.Container.IContainer;

namespace Web.Adapters.Container;
internal class AutofacContainer : IContainer
{
    private readonly Autofac.IContainer container;

    public AutofacContainer(Autofac.IContainer container)
    {
        this.container = container;
    }

    public T Resolve<T>() where T : notnull => container.Resolve<T>();
}
