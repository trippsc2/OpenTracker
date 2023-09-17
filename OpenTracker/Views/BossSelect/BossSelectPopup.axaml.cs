using System;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.Views.BossSelect;

public sealed class BossSelectPopup : ReactiveUserControl<BossSelectPopupVM>
{
    private Popup Popup => this.FindControl<Popup>("Popup");
    private LayoutTransformControl LayoutTransformControl =>
        this.FindControl<LayoutTransformControl>("LayoutTransformControl");
    private ItemsControl Buttons => this.FindControl<ItemsControl>("Buttons");
    
    public BossSelectPopup()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            Buttons.ItemsPanel = new FuncTemplate<IPanel>(() => new WrapPanel
            {
                ItemWidth = 34,
                ItemHeight = 40,
                MaxWidth = 136,
                MaxHeight = 120,
                Margin = new Thickness(0),
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            });
            
            Disposable
                .Create(() => Buttons.ItemsPanel = null)
                .DisposeWith(disposables);

            this.Bind(ViewModel,
                    vm => vm.PopupOpen,
                    v => v.Popup.IsOpen)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Buttons,
                    v => v.Buttons.Items)
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
}