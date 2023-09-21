using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using OpenTracker.Models.Items;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

/// <summary>
/// This class contains the prize section icon control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class PrizeSectionIconVM : ViewModel, ISectionIconVM
{
    private readonly IPrizeDictionary _prizes;
    private readonly IUndoRedoManager _undoRedoManager;

    private IPrizeSection Section { get; }

    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;
        
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate PrizeSectionIconVM Factory(IPrizeSection section);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="prizes">
    /// The prize dictionary.
    /// </param>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="section">
    /// The prize section to be presented.
    /// </param>
    public PrizeSectionIconVM(IPrizeDictionary prizes, IUndoRedoManager undoRedoManager, IPrizeSection section)
    {
        _prizes = prizes;
        _undoRedoManager = undoRedoManager;

        Section = section;
            
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.Section.PrizePlacement.Prize,
                    x => x.Section.Available,
                    GetImageSource)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
        });
    }

    private string GetImageSource(IItem? prizeItem, int _)
    {
        return $"avares://OpenTracker/Assets/Images/Prizes/{GetPrizeItemTypeName(prizeItem)}{GetImageSourceSuffix()}";
    }

    private string GetPrizeItemTypeName(IItem? prizeItem)
    {
        return prizeItem is not null
            ? _prizes.First(x => x.Value == prizeItem).Key.ToString().ToLowerInvariant()
            : "unknown";
    }

    private string GetImageSourceSuffix()
    {
        return Section.IsAvailable() ? "0.png" : "1.png";
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        switch (e.InitialPressMouseButton)
        {
            case MouseButton.Left:
                TogglePrize((e.KeyModifiers & KeyModifiers.Control) > 0);
                break;
            case MouseButton.Right:
                ChangePrize();
                break;
            case MouseButton.None:
            case MouseButton.Middle:
            case MouseButton.XButton1:
            case MouseButton.XButton2:
            default:
                break;
        }
    }

    private void TogglePrize(bool force = false)
    {
        _undoRedoManager.NewAction(Section.CreateTogglePrizeSectionAction(force));
    }

    private void ChangePrize()
    {
        _undoRedoManager.NewAction(Section.PrizePlacement.CreateChangePrizeAction());
    }

}