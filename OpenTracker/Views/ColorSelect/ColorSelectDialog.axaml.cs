using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.ColorSelect;
using ReactiveUI;

namespace OpenTracker.Views.ColorSelect;

public sealed class ColorSelectDialog : ReactiveWindow<ColorSelectDialogVM>
{
    private ItemsControl FontColors => this.FindControl<ItemsControl>(nameof(FontColors));
    private ItemsControl AccessibilityColors => this.FindControl<ItemsControl>(nameof(AccessibilityColors));
    private ItemsControl ConnectorColors => this.FindControl<ItemsControl>(nameof(ConnectorColors));
    
    public ColorSelectDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(disposables =>
        {
            FontColors.ItemsPanel =
                new FuncTemplate<IPanel>(() => new StackPanel { Orientation = Orientation.Vertical });
            AccessibilityColors.ItemsPanel =
                new FuncTemplate<IPanel>(() => new StackPanel { Orientation = Orientation.Vertical });
            ConnectorColors.ItemsPanel =
                new FuncTemplate<IPanel>(() => new StackPanel { Orientation = Orientation.Vertical });
            
            this.OneWayBind(ViewModel,
                    vm => vm.FontColors,
                    v => v.FontColors.Items)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.AccessibilityColors,
                    v => v.AccessibilityColors.Items)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.ConnectorColors,
                    v => v.ConnectorColors.Items)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}