using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Input;
using OpenTracker.Autofac;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Items.Adapters;

[DependencyInjection]
public sealed class DropdownAdapter : ViewModel, IItemAdapter
{
    private readonly IUndoRedoManager _undoRedoManager;

    private IDropdown Dropdown { get; }

    [ObservableAsProperty]
    public bool Visible { get; }
    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;

    public string? Label => null;
    public string LabelColor => "#ffffffff";
    public IBossSelectPopupVM? BossSelect => null;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

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

        this.WhenAnyValue(x => x.Dropdown.Requirement.Met)
            .ToPropertyEx(this, x => x.Visible);
        
        this.WhenAnyValue(x => x.Dropdown.Checked)
            .Select(x => imageSourceBase + (x ? "1" : "0") + ".png")
            .ToPropertyEx(this, x => x.ImageSource);

        HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);
    }

    /// <summary>
    /// Creates a new undoable action to check the dropdown to the undo/redo manager.
    /// </summary>
    private void CheckDropdown()
    {
        _undoRedoManager.NewAction(Dropdown.CreateCheckDropdownAction());
    }

    /// <summary>
    /// Creates a new undoable action to uncheck the dropdown to the undo/redo manager.
    /// </summary>
    private void UncheckDropdown()
    {
        _undoRedoManager.NewAction(Dropdown.CreateUncheckDropdownAction());
    }

    /// <summary>
    /// Handles the dropdown being clicked.
    /// </summary>
    /// <param name="e">
    /// The pointer released event args.
    /// </param>
    private void HandleClickImpl(PointerReleasedEventArgs e)
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
}