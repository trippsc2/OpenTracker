using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Items.Adapters;

[DependencyInjection]
public sealed class DropdownAdapter : ViewModel, IItemAdapter
{
    private readonly IUndoRedoManager _undoRedoManager;

    private IDropdown Dropdown { get; }

    public string? Label => null;
    public SolidColorBrush? LabelColor => null;
    public BossSelectPopupVM? BossSelect => null;

    [ObservableAsProperty]
    public bool Visible { get; }
    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate DropdownAdapter Factory(IDropdown dropdown, string imageSourceBase);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="imageSourceBase">
    /// A string representing the base image source.
    /// </param>
    /// <param name="dropdown">
    /// An item that is to be represented by this control.
    /// </param>
    public DropdownAdapter(IUndoRedoManager undoRedoManager, IDropdown dropdown, string imageSourceBase)
    {
        _undoRedoManager = undoRedoManager;

        Dropdown = dropdown;

        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Dropdown.Requirement.Met)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Visible)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Dropdown.Checked)
                .Select(x => imageSourceBase + (x ? "1" : "0") + ".png")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
        });
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        switch (e.InitialPressMouseButton)
        {
            case MouseButton.Left:
                CheckDropdown();
                break;
            case MouseButton.Right:
                UncheckDropdown();
                break;
            case MouseButton.None:
            case MouseButton.Middle:
            case MouseButton.XButton1:
            case MouseButton.XButton2:
            default:
                break;
        }
    }

    private void CheckDropdown()
    {
        _undoRedoManager.NewAction(Dropdown.CreateCheckDropdownAction());
    }

    private void UncheckDropdown()
    {
        _undoRedoManager.NewAction(Dropdown.CreateUncheckDropdownAction());
    }

}