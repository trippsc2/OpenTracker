using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.UIPanels;
using ReactiveUI;

namespace OpenTracker.Views.UIPanels;

public sealed class UIPanel : ReactiveUserControl<UIPanelVM>
{
    private Border BodyBorder => this.FindControl<Border>("BodyBorder");

    public UIPanel()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.AlternateBodyColor,
                    v => v.BodyBorder.Background,
                    selector: GetBodyBorderBackground)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private IBrush GetBodyBorderBackground(bool alternateBodyColor)
    {
        var key = alternateBodyColor ? "AlternativeBackground" : "StandardBackground";

        var resource = this.FindResource(key);

        return resource as IBrush ?? Brushes.Gray;
    }
}