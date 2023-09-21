using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels;

namespace OpenTracker.Views;

public sealed class StatusBar : ReactiveUserControl<StatusBarVM>
{
    public StatusBar()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}