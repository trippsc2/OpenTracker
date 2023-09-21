using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This class contains the logic to adapt dungeon prize data to an item control.
/// </summary>
[DependencyInjection]
public sealed class PrizeAdapter : ViewModel, IItemAdapter
{
    private static readonly Dictionary<(PrizeType? prize, bool isAvailable), string> ImageSourceValues = new()
    {
        { (null, true), "avares://OpenTracker/Assets/Images/Prizes/unknown0.png" },
        { (null, false), "avares://OpenTracker/Assets/Images/Prizes/unknown1.png" },
        { (PrizeType.Aga1, true), "avares://OpenTracker/Assets/Images/Prizes/aga10.png" },
        { (PrizeType.Aga1, false), "avares://OpenTracker/Assets/Images/Prizes/aga11.png" },
        { (PrizeType.Aga2, true), "avares://OpenTracker/Assets/Images/Prizes/aga20.png" },
        { (PrizeType.Aga2, false), "avares://OpenTracker/Assets/Images/Prizes/aga11.png" },
        { (PrizeType.Crystal, true), "avares://OpenTracker/Assets/Images/Prizes/crystal0.png" },
        { (PrizeType.Crystal, false), "avares://OpenTracker/Assets/Images/Prizes/crystal1.png" },
        { (PrizeType.GreenPendant, true), "avares://OpenTracker/Assets/Images/Prizes/greenpendant0.png" },
        { (PrizeType.GreenPendant, false), "avares://OpenTracker/Assets/Images/Prizes/greenpendant1.png" },
        { (PrizeType.Pendant, true), "avares://OpenTracker/Assets/Images/Prizes/pendant0.png" },
        { (PrizeType.Pendant, false), "avares://OpenTracker/Assets/Images/Prizes/pendant1.png" },
        { (PrizeType.RedCrystal, true), "avares://OpenTracker/Assets/Images/Prizes/redcrystal0.png" },
        { (PrizeType.RedCrystal, false), "avares://OpenTracker/Assets/Images/Prizes/redcrystal1.png" }
    };

    private readonly IUndoRedoManager _undoRedoManager;

    private IPrizeSection Section { get; }

    public string? Label => null;
    public SolidColorBrush? LabelColor => null;
    public BossSelectPopupVM? BossSelect => null;

    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate PrizeAdapter Factory(IPrizeSection section);
    
    public PrizeAdapter(IPrizeDictionary prizes, IUndoRedoManager undoRedoManager, IPrizeSection section)
    {
        _undoRedoManager = undoRedoManager;

        Section = section;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick); 

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    x => x.Section.PrizePlacement.Prize,
                    x => x.Section.Available,
                    (item, _) =>
                        (item is not null
                            ? (PrizeType?) prizes.First(x => x.Value == item).Key
                            : null,
                            Section.IsAvailable()))
                .Select(x => ImageSourceValues[x])
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
                TogglePrize();
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

    private void TogglePrize()
    {
        _undoRedoManager.NewAction(Section.CreateTogglePrizeSectionAction(true));
    }

    private void ChangePrize()
    {
        _undoRedoManager.NewAction(Section.PrizePlacement.CreateChangePrizeAction());
    }

}