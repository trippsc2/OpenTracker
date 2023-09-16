using System;
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
    private Border ControlBorder => this.FindControl<Border>("ControlBorder");
    private TextBlock TitleText => this.FindControl<TextBlock>("TitleText");
    private ContentControl ModeSettings => this.FindControl<ContentControl>("ModeSettings");
    private LayoutTransformControl LayoutTransformControl =>
        this.FindControl<LayoutTransformControl>("LayoutTransformControl");
    private Border BodyBorder => this.FindControl<Border>("BodyBorder");
    private ContentControl Body => this.FindControl<ContentControl>("Body");
    
    public UIPanel()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.Visible,
                    v => v.ControlBorder.IsVisible)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Title,
                    v => v.TitleText.Text)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.ModeSettings,
                    v => v.ModeSettings.Content)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.AlternateBodyColor,
                    v => v.BodyBorder.Background,
                    selector: GetBodyBorderBackground)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Body,
                    v => v.Body.Content)
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.ViewModel!.Scale)
                .Subscribe(x =>
                {
                    ((ScaleTransform)LayoutTransformControl.LayoutTransform).ScaleX = x;
                    ((ScaleTransform)LayoutTransformControl.LayoutTransform).ScaleY = x;
                })
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