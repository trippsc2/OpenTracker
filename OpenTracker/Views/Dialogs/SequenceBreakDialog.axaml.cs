using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dialogs;
using ReactiveUI;

namespace OpenTracker.Views.Dialogs;

public sealed class SequenceBreakDialog : ReactiveWindow<SequenceBreakDialogVM>
{
    private ItemsControl BombDuplicationItemsControl =>
        this.FindControl<ItemsControl>(nameof(BombDuplicationItemsControl));
    private ItemsControl BombJumpsItemsControl => this.FindControl<ItemsControl>(nameof(BombJumpsItemsControl));
    private ItemsControl DarkRoomsItemsControl => this.FindControl<ItemsControl>(nameof(DarkRoomsItemsControl));
    private ItemsControl FakeFlippersItemsControl => this.FindControl<ItemsControl>(nameof(FakeFlippersItemsControl));
    private ItemsControl SuperBunnyItemsControl => this.FindControl<ItemsControl>(nameof(SuperBunnyItemsControl));
    private ItemsControl OtherItemsControl => this.FindControl<ItemsControl>(nameof(OtherItemsControl));
    
    public SequenceBreakDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.WhenActivated(disposables =>
        {
            BombDuplicationItemsControl.ItemsPanel = new FuncTemplate<IPanel>(() => new UniformGrid { Columns = 3 });
            BombJumpsItemsControl.ItemsPanel = new FuncTemplate<IPanel>(() => new UniformGrid { Columns = 3 });
            DarkRoomsItemsControl.ItemsPanel = new FuncTemplate<IPanel>(() => new UniformGrid { Columns = 3 });
            FakeFlippersItemsControl.ItemsPanel = new FuncTemplate<IPanel>(() => new UniformGrid { Columns = 3 });
            SuperBunnyItemsControl.ItemsPanel = new FuncTemplate<IPanel>(() => new UniformGrid { Columns = 3 });
            OtherItemsControl.ItemsPanel = new FuncTemplate<IPanel>(() => new UniformGrid { Columns = 3 });

            Disposable
                .Create(() =>
                {
                    BombDuplicationItemsControl.ItemsPanel = null;
                    BombJumpsItemsControl.ItemsPanel = null;
                    DarkRoomsItemsControl.ItemsPanel = null;
                    FakeFlippersItemsControl.ItemsPanel = null;
                    SuperBunnyItemsControl.ItemsPanel = null;
                    OtherItemsControl.ItemsPanel = null;
                })
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                    vm => vm.BombDuplication,
                    v => v.BombDuplicationItemsControl.Items)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.BombJumps,
                    v => v.BombJumpsItemsControl.Items)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.DarkRooms,
                    v => v.DarkRoomsItemsControl.Items)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.FakeFlippersWaterWalk,
                    v => v.FakeFlippersItemsControl.Items)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.SuperBunny,
                    v => v.SuperBunnyItemsControl.Items)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Other,
                    v => v.OtherItemsControl.Items)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}