using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ValueConverters;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.Views.BossSelect;

public sealed class BossSelectButton : ReactiveUserControl<BossSelectButtonVM>
{
    private Button Button => this.FindControl<Button>("Button");
    private Image Image => this.FindControl<Image>("Image");

    public BossSelectButton()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.ImageSource,
                    v => v.Image.Source,
                    vmToViewConverterOverride: new StringToImageValueConverter())
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel,
                    vm => vm.ChangeBossCommand,
                    v => v.Button,
                    vm => vm.Boss)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}