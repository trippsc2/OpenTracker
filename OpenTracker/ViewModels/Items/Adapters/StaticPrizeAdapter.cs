using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Items.Adapters;

/// <summary>
/// This class contains the logic to adapt prize data to an item control.
/// </summary>
[DependencyInjection]
public sealed class StaticPrizeAdapter : ViewModel, IItemAdapter
{
    private readonly IUndoRedoManager _undoRedoManager;

    private IPrizeSection Section { get; }
    
    public string? Label => null;
    public SolidColorBrush? LabelColor => null;
    public BossSelectPopupVM? BossSelect => null;

    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;
    
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    public delegate StaticPrizeAdapter Factory(IPrizeSection section, string imageSourceBase);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="imageSourceBase">
    /// A string representing the base image source.
    /// </param>
    /// <param name="section">
    /// An item that is to be represented by this control.
    /// </param>
    public StaticPrizeAdapter(IUndoRedoManager undoRedoManager, IPrizeSection section, string imageSourceBase)
    {
        _undoRedoManager = undoRedoManager;
        var imageSourceBase1 = imageSourceBase;
        Section = section;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        
        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Section.Available)
                .Select(_ => Section.IsAvailable()
                    ? $"{imageSourceBase1}0.png"
                    : $"{imageSourceBase1}1.png")
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
                CollectSection();
                break;
            case MouseButton.Right:
                UncollectSection();
                break;
            case MouseButton.None:
            case MouseButton.Middle:
            case MouseButton.XButton1:
            case MouseButton.XButton2:
            default:
                break;
        }
    }

    private void CollectSection()
    {
        _undoRedoManager.NewAction(Section.CreateCollectSectionAction(true));
    }

    private void UncollectSection()
    {
        _undoRedoManager.NewAction(Section.CreateUncollectSectionAction());
    }
}