using System.Reflection;
using OpenTracker.Autofac;
using OpenTracker.Utils.Dialog;

namespace OpenTracker.ViewModels.Dialogs;

[DependencyInjection(SingleInstance = true)]
public sealed class AboutDialogVM : DialogViewModelBase
{
    public static string Version { get; } = $"v{Assembly.GetExecutingAssembly().GetName().Version}";
}