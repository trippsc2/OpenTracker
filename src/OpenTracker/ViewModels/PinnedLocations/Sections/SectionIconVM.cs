using System.Globalization;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.PinnedLocations.Sections;

[DependencyInjection]
public sealed class SectionIconVM : ViewModel, ISectionIconVM
{
    private readonly IUndoRedoManager _undoRedoManager;
        
    private ISectionIconImageProvider ImageProvider { get; }
    private ISection Section { get; }
    
    public bool LabelVisible { get; }

    [ObservableAsProperty]
    public string ImageSource { get; } = string.Empty;
    [ObservableAsProperty]
    public string Label => Section.Available.ToString(CultureInfo.InvariantCulture);
        
    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="imageProvider">
    /// The section icon image control provider.
    /// </param>
    /// <param name="section">
    /// The section data.
    /// </param>
    /// <param name="labelVisible">
    /// A boolean representing whether the label is visible.
    /// </param>
    public SectionIconVM(
        IUndoRedoManager undoRedoManager,
        ISectionIconImageProvider imageProvider,
        ISection section,
        bool labelVisible)
    {
        ImageProvider = imageProvider;
        Section = section;

        LabelVisible = labelVisible;
        _undoRedoManager = undoRedoManager;

        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);
        
        this.WhenActivated(disposables =>
        {
            ImageProvider.Activator
                .Activate()
                .DisposeWith(disposables);

            this.WhenAnyValue(x => x.ImageProvider.ImageSource)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Section.Available)
                .Select(x => x.ToString().ToLowerInvariant())
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Label)
                .DisposeWith(disposables);
        });
    }

    private void HandleClick(PointerReleasedEventArgs e)
    {
        switch (e.InitialPressMouseButton)
        {
            case MouseButton.Left:
                CollectSection((e.KeyModifiers & KeyModifiers.Control) > 0);
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

    private void CollectSection(bool force)
    {
        _undoRedoManager.NewAction(Section.CreateCollectSectionAction(force));
    }

    /// <summary>
    /// Creates an undoable action to un-collect the section and send it to the undo/redo manager.
    /// </summary>
    private void UncollectSection()
    {
        _undoRedoManager.NewAction(Section.CreateUncollectSectionAction());
    }

}