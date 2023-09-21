using Autofac;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
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
        var autofacResolver = builder.UseAutofacDependencyResolver();
        builder.RegisterInstance(autofacResolver);
        autofacResolver.InitializeReactiveUI();
        
        RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
        
        Locator.CurrentMutable.RegisterConstant(
            new AvaloniaActivationForViewFetcher(),
            typeof(IActivationForViewFetcher));
        Locator.CurrentMutable.RegisterConstant(
            new AutoDataTemplateBindingHook(),
            typeof(IPropertyBindingHook));
        
        return builder;
    }
}