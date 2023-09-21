using System.Reflection;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.Dialogs;

[DependencyInjection(SingleInstance = true)]
public sealed class AboutDialogVM : ViewModel
{
    public string Version { get; } = $"v{Assembly.GetExecutingAssembly().GetName().Version}";
}