using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Autofac;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This class contains the logic to adapt dungeon prize data to an item control.
/// </summary>
[DependencyInjection]
public sealed class PrizeAdapter : ViewModel, IItemAdapter
{
    private readonly IPrizeDictionary _prizes;
    private readonly IUndoRedoManager _undoRedoManager;

    private readonly IPrizeSection _section;

    public string ImageSource
    {
        get
        {
            var sb = new StringBuilder();
            sb.Append("avares://OpenTracker/Assets/Images/Prizes/");

            sb.Append(_section.PrizePlacement.Prize is null ?
                "unknown" : _prizes.FirstOrDefault(
                        x => x.Value == _section.PrizePlacement.Prize).Key.ToString()
                    .ToLowerInvariant());

            sb.Append(_section.IsAvailable() ? "0" : "1");
            sb.Append(".png");

            return sb.ToString();
        }
    }
        
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

    public delegate PrizeAdapter Factory(IPrizeSection section);

    public string? Label { get; } = null;
    public string LabelColor { get; } = "#ffffffff";
        
    public IBossSelectPopupVM? BossSelect { get; } = null;

    public PrizeAdapter(IPrizeDictionary prizes, IUndoRedoManager undoRedoManager, IPrizeSection section)
    {
        _prizes = prizes;
        _undoRedoManager = undoRedoManager;

        _section = section;
            
        HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

        _section.PropertyChanged += OnSectionChanged;
        _section.PrizePlacement.PropertyChanged += OnPrizeChanged;
    }
        

    /// <summary>
    /// Subscribes to the PropertyChanged event on the IPrizePlacement interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnPrizeChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IPrizePlacement.Prize))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
        }
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the ISection interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnSectionChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ISection.Available))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
        }
    }

    /// <summary>
    /// Creates an undoable action to toggle the prize section and sends it to the undo/redo manager.
    /// </summary>
    private void TogglePrize()
    {
        _undoRedoManager.NewAction(_section.CreateTogglePrizeSectionAction(true));
    }

    /// <summary>
    /// Creates an undoable action to change the prize and sends it to the undo/redo manager.
    /// </summary>
    private void ChangePrize()
    {
        _undoRedoManager.NewAction(_section.PrizePlacement.CreateChangePrizeAction());
    }

    /// <summary>
    /// Handles clicking the control.
    /// </summary>
    /// <param name="e">
    /// The pointer released event args.
    /// </param>
    private void HandleClickImpl(PointerReleasedEventArgs e)
    {
        switch (e.InitialPressMouseButton)
        {
            case MouseButton.Left:
                TogglePrize();
                break;
            case MouseButton.Right:
                ChangePrize();
                break;
        }
    }
}