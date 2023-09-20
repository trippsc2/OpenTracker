using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.BossSelect;

namespace OpenTracker.Views.BossSelect;

public sealed class BossSelectPopup : ReactiveUserControl<BossSelectPopupVM>
{
    public BossSelectPopup()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}