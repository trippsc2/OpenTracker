using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels;

namespace OpenTracker.Views;

public sealed class ModeSettings : ReactiveUserControl<ModeSettingsVM>
{
    public ModeSettings()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}