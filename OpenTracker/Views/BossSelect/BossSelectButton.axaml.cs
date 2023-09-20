using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.BossSelect;

namespace OpenTracker.Views.BossSelect;

public sealed class BossSelectButton : ReactiveUserControl<BossSelectButtonVM>
{
    public BossSelectButton()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}