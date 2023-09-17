using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.ColorSelect;
using ReactiveUI;

namespace OpenTracker.Views.ColorSelect;

public sealed class ColorSelectControl : ReactiveUserControl<ColorSelectControlVM>
{
    private ToggleButton ColorPickerToggleButton => this.FindControl<ToggleButton>("ColorPickerToggleButton");
    private TextBlock Label => this.FindControl<TextBlock>("Label");
    private Border ColorPickerBorder => this.FindControl<Border>("ColorPickerBorder");
    private ColorPickerControl ColorPickerControl => this.FindControl<ColorPickerControl>("ColorPickerControl");
    
    public ColorSelectControl()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.Color,
                    v => v.ColorPickerToggleButton.Background)
                .DisposeWith(disposables);
            this.Bind(ViewModel,
                    vm => vm.PickerOpen,
                    v => v.ColorPickerToggleButton.IsChecked)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Label,
                    v => v.Label.Text)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.PickerOpen,
                    v => v.ColorPickerBorder.IsVisible)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Color,
                    v => v.ColorPickerControl.DataContext)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}