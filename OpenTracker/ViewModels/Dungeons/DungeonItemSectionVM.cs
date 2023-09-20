using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Input;
using Avalonia.Media;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using Reactive.Bindings;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveCommand = ReactiveUI.ReactiveCommand;

namespace OpenTracker.ViewModels.Dungeons;

/// <summary>
/// This class contains dungeon items small items panel control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class DungeonItemSectionVM : ViewModel, IDungeonItemSectionVM
{
    private static readonly SolidColorBrush NormalFontColor = SolidColorBrush.Parse("#ffffff");
    
    private readonly IUndoRedoManager _undoRedoManager;

    private IColorSettings ColorSettings { get; }
    private ISection Section { get; }
    
    [ObservableAsProperty]
    public ReactiveProperty<SolidColorBrush> AccessibilityColor { get; } = default!;
    [ObservableAsProperty]
    public SolidColorBrush FontColor { get; } = NormalFontColor;
    [ObservableAsProperty]
    public string ImageSource { get; } = "avares://OpenTracker/Assets/Images/chest0.png";
    [ObservableAsProperty]
    public string NumberString { get; } = "0";

    public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="colorSettings">
    /// The color settings data.
    /// </param>
    /// <param name="undoRedoManager">
    /// The undo/redo manager.
    /// </param>
    /// <param name="section">
    /// The dungeon section to be represented.
    /// </param>
    public DungeonItemSectionVM(IColorSettings colorSettings, IUndoRedoManager undoRedoManager, ISection section)
    {
        _undoRedoManager = undoRedoManager;
        ColorSettings = colorSettings;
        Section = section;
        
        HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.Section.Accessibility)
                .Select(x => ColorSettings.AccessibilityColors[x])
                .ToPropertyEx(this, x => x.AccessibilityColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.Section.Available,
                    x => x.Section.Accessibility,
                    x => x.AccessibilityColor,
                    x => x.AccessibilityColor.Value,
                    (available, _, _, color) => available == 0 ? NormalFontColor : color)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.FontColor)
                .DisposeWith(disposables);
            this.WhenAnyValue(
                    x => x.Section.Available,
                    x => x.Section.Accessibility,
                    (_, accessibility) =>
                        Section.IsAvailable()
                            ? accessibility is AccessibilityLevel.None or AccessibilityLevel.Inspect
                                ? "avares://OpenTracker/Assets/Images/chest0.png"
                                : "avares://OpenTracker/Assets/Images/chest1.png"
                            : "avares://OpenTracker/Assets/Images/chest2.png")
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ImageSource)
                .DisposeWith(disposables);
            this.WhenAnyValue(x => x.Section.Available)
                .Select(x => x.ToString())
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.NumberString)
                .DisposeWith(disposables);
        });
    }

    /// <summary>
    /// Creates an undoable action to collect the section and sends it to the undo/redo manager.
    /// </summary>
    /// <param name="force">
    /// A boolean representing whether the logic should be ignored.
    /// </param>
    private void CollectSection(bool force)
    {
        _undoRedoManager.NewAction(Section.CreateCollectSectionAction(force));
    }

    /// <summary>
    /// Creates an undoable action to un-collect the section and sends it to the undo/redo manager.
    /// </summary>
    private void UncollectSection()
    {
        _undoRedoManager.NewAction(Section.CreateUncollectSectionAction());
    }

    /// <summary>
    /// Handles clicking the control.
    /// </summary>
    /// <param name="e">
    /// The pointer released event args.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
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
}