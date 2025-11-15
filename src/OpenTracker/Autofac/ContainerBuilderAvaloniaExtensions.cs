using Autofac;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using Splat.Autofac;

namespace OpenTracker.Autofac;

/// <summary>
/// Extends <see cref="ContainerBuilder"/> to configure interfaces for Avalonia and Splat. 
/// </summary>
public static class ContainerBuilderAvaloniaExtensions
{
    public static ContainerBuilder ConfigureAvalonia(this ContainerBuilder builder)
{
    ConfigureAutofacResolver(builder);
    ConfigureReactiveUIServices();
    return builder;
}

private static void ConfigureAutofacResolver(ContainerBuilder builder)
{
    var autofacResolver = builder.UseAutofacDependencyResolver();
    builder.RegisterInstance(autofacResolver);
    RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
}

private static void ConfigureReactiveUIServices()
{
    Locator.CurrentMutable.RegisterConstant(
        new AvaloniaActivationForViewFetcher(),
        typeof(IActivationForViewFetcher));
    Locator.CurrentMutable.RegisterConstant(
        new AutoDataTemplateBindingHook(),
        typeof(IPropertyBindingHook));
}
}