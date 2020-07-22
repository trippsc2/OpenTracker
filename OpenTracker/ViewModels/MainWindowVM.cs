using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using Newtonsoft.Json;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.ViewModels.MapAreaControls;
using OpenTracker.ViewModels.SequenceBreaks;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the main window.
    /// </summary>
    public class MainWindowVM : ViewModelBase, IAutoTrackerAccess, IColorSelectAccess,
        IDynamicLayout, IOpen, ISave, ISaveAppSettings, IBounds, ISequenceBreakAccess
    {
        private readonly IDialogService _dialogService;
        private AutoTrackerDialogVM _autoTrackerDialog;

        public static ObservableCollection<MarkingSelectControlVM> NonEntranceMarkingSelect { get; } = 
            new ObservableCollection<MarkingSelectControlVM>();
        public static ObservableCollection<MarkingSelectControlVM> EntranceMarkingSelect { get; } = 
            new ObservableCollection<MarkingSelectControlVM>();

        public bool? Maximized
        {
            get => AppSettings.Instance.Maximized;
            set => AppSettings.Instance.Maximized = value;
        }
        public double? X
        {
            get => AppSettings.Instance.X;
            set => AppSettings.Instance.X = value;
        }
        public double? Y
        {
            get => AppSettings.Instance.Y;
            set => AppSettings.Instance.Y = value;
        }
        public double? Width
        {
            get => AppSettings.Instance.Width;
            set => AppSettings.Instance.Width = value;
        }
        public double? Height
        {
            get => AppSettings.Instance.Height;
            set => AppSettings.Instance.Height = value;
        }

        private Orientation _orientation;
        public Orientation Orientation
        {
            get => _orientation;
            private set => this.RaiseAndSetIfChanged(ref _orientation, value);
        }

        public Dock UIPanelDock
        {
            get
            {
                if (AppSettings.Instance.LayoutOrientation.HasValue)
                {
                    switch (AppSettings.Instance.LayoutOrientation.Value)
                    {
                        case Orientation.Horizontal:
                            {
                                return AppSettings.Instance.HorizontalUIPanelPlacement switch
                                {
                                    VerticalAlignment.Top => Dock.Top,
                                    _ => Dock.Bottom,
                                };
                            }
                        case Orientation.Vertical:
                            {
                                return AppSettings.Instance.VerticalUIPanelPlacement switch
                                {
                                    HorizontalAlignment.Left => Dock.Left,
                                    _ => Dock.Right,
                                };
                            }
                    }
                }
                else
                {
                    switch (Orientation)
                    {
                        case Orientation.Horizontal:
                            {
                                return AppSettings.Instance.HorizontalUIPanelPlacement switch
                                {
                                    VerticalAlignment.Top => Dock.Top,
                                    _ => Dock.Bottom,
                                };
                            }
                        case Orientation.Vertical:
                            {
                                return AppSettings.Instance.VerticalUIPanelPlacement switch
                                {
                                    HorizontalAlignment.Left => Dock.Left,
                                    _ => Dock.Right,
                                };
                            }
                    }
                }

                return Dock.Right;
            }
        }
        public HorizontalAlignment UIPanelHorizontalAlignment
        {
            get
            {
                return UIPanelDock switch
                {
                    Dock.Left => HorizontalAlignment.Left,
                    Dock.Right => HorizontalAlignment.Right,
                    _ => HorizontalAlignment.Stretch,
                };
            }
        }
        public VerticalAlignment UIPanelVerticalAlignment
        {
            get
            {
                return UIPanelDock switch
                {
                    Dock.Bottom => VerticalAlignment.Bottom,
                    Dock.Top => VerticalAlignment.Top,
                    _ => VerticalAlignment.Stretch,
                };
            }
        }
        public Dock UIPanelOrientationDock
        {
            get
            {
                switch (UIPanelDock)
                {
                    case Dock.Left:
                    case Dock.Right:
                        {
                            return AppSettings.Instance.VerticalItemsPlacement switch
                            {
                                VerticalAlignment.Top => Dock.Top,
                                _ => Dock.Bottom,
                            };
                        }
                    case Dock.Bottom:
                    case Dock.Top:
                        {
                            return AppSettings.Instance.HorizontalItemsPlacement switch
                            {
                                HorizontalAlignment.Left => Dock.Left,
                                _ => Dock.Right,
                            };
                        }
                }

                return Dock.Right;
            }
        }

        public TopMenuControlVM TopMenu { get; }
        public ItemsPanelControlVM ItemsPanel { get; }
        public LocationsPanelControlVM LocationsPanel { get; }
        public MapAreaControlVM MapArea { get; }

        public ReactiveCommand<Unit, Unit> OpenResetDialogCommand { get; }
        public ReactiveCommand<Unit, Unit> UndoCommand { get; }
        public ReactiveCommand<Unit, Unit> RedoCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand { get; }
        public ReactiveCommand<Unit, Unit> ToggleShowItemCountsOnMapCommand { get; }
        public ReactiveCommand<string, Unit> SetLayoutOrientationCommand { get; }
        public ReactiveCommand<string, Unit> SetMapOrientationCommand { get; }
        public ReactiveCommand<string, Unit> SetHorizontalUIPanelPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetVerticalUIPanelPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetHorizontalItemsPlacementCommand { get; }
        public ReactiveCommand<string, Unit> SetVerticalItemsPlacementCommand { get; }

        private readonly ObservableAsPropertyHelper<bool> _isOpeningResetDialog;
        public bool IsOpeningResetDialog =>
            _isOpeningResetDialog.Value;

        private bool _canUndo;
        public bool CanUndo
        {
            get => _canUndo;
            private set => this.RaiseAndSetIfChanged(ref _canUndo, value);
        }

        private bool _canRedo;
        public bool CanRedo
        {
            get => _canRedo;
            private set => this.RaiseAndSetIfChanged(ref _canRedo, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dialogService">
        /// The dialog service.
        /// </param>
        public MainWindowVM(IDialogService dialogService) : this()
        {
            _dialogService = dialogService;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowVM()
        {
            UndoRedoManager.Instance.UndoableActions.CollectionChanged += OnUndoChanged;
            UndoRedoManager.Instance.RedoableActions.CollectionChanged += OnRedoChanged;

            OpenResetDialogCommand = ReactiveCommand.CreateFromObservable(OpenResetDialogAsync);
            OpenResetDialogCommand.IsExecuting.ToProperty(
                this, x => x.IsOpeningResetDialog, out _isOpeningResetDialog);

            UndoCommand = ReactiveCommand.Create(Undo, this.WhenAnyValue(x => x.CanUndo));
            RedoCommand = ReactiveCommand.Create(Redo, this.WhenAnyValue(x => x.CanRedo));
            ToggleDisplayAllLocationsCommand = ReactiveCommand.Create(ToggleDisplayAllLocations);
            PropertyChanged += OnPropertyChanged;

            TopMenu = new TopMenuControlVM(this);
            ItemsPanel = new ItemsPanelControlVM(this);
            LocationsPanel = new LocationsPanelControlVM(this);
            MapArea = new MapAreaControlVM(this);

            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;

            if (NonEntranceMarkingSelect.Count == 0)
            {
                for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
                {
                    switch ((MarkingType)i)
                    {
                        case MarkingType.Sword:
                        case MarkingType.Shield:
                        case MarkingType.Mail:
                        case MarkingType.Boots:
                        case MarkingType.Gloves:
                        case MarkingType.Flippers:
                        case MarkingType.MoonPearl:
                        case MarkingType.Bow:
                        case MarkingType.SilverArrows:
                        case MarkingType.Boomerang:
                        case MarkingType.RedBoomerang:
                        case MarkingType.Hookshot:
                        case MarkingType.Bomb:
                        case MarkingType.Mushroom:
                        case MarkingType.FireRod:
                        case MarkingType.IceRod:
                        case MarkingType.Bombos:
                        case MarkingType.Ether:
                        case MarkingType.Powder:
                        case MarkingType.Lamp:
                        case MarkingType.Hammer:
                        case MarkingType.Flute:
                        case MarkingType.Net:
                        case MarkingType.Book:
                        case MarkingType.Shovel:
                        case MarkingType.SmallKey:
                        case MarkingType.Bottle:
                        case MarkingType.CaneOfSomaria:
                        case MarkingType.CaneOfByrna:
                        case MarkingType.Cape:
                        case MarkingType.Mirror:
                        case MarkingType.HalfMagic:
                        case MarkingType.BigKey:
                            {
                                NonEntranceMarkingSelect.Add(new MarkingSelectControlVM((MarkingType)i));
                            }
                            break;
                        case MarkingType.Quake:
                            {
                                NonEntranceMarkingSelect.Add(new MarkingSelectControlVM((MarkingType)i));
                                NonEntranceMarkingSelect.Add(new MarkingSelectControlVM(null));
                            }
                            break;
                    }
                }
            }

            if (EntranceMarkingSelect.Count == 0)
            {
                for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
                {
                    EntranceMarkingSelect.Add(new MarkingSelectControlVM((MarkingType)i));
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on this class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Orientation))
            {
                UpdateUIPanelDock();
            }

            if (e.PropertyName == nameof(UIPanelDock))
            {
                this.RaisePropertyChanged(nameof(UIPanelHorizontalAlignment));
                this.RaisePropertyChanged(nameof(UIPanelVerticalAlignment));
                UpdateUIPanelOrientationDock();
            }
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the observable stack of undoable actions.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnUndoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanUndo();
        }

        /// <summary>
        /// Subscribes to the CollectionChanged event on the observable stack of redoable actions.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRedoChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCanRedo();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AppSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.LayoutOrientation) ||
                e.PropertyName == nameof(AppSettings.HorizontalUIPanelPlacement) ||
                e.PropertyName == nameof(AppSettings.VerticalUIPanelPlacement))
            {
                UpdateUIPanelDock();
            }

            if (e.PropertyName == nameof(AppSettings.HorizontalItemsPlacement) ||
                e.PropertyName == nameof(AppSettings.VerticalItemsPlacement))
            {
                UpdateUIPanelOrientationDock();
            }
        }

        /// <summary>
        /// Updates the CanUndo property with a value representing whether there are objects in the
        /// Undoable stack.
        /// </summary>
        private void UpdateCanUndo()
        {
            CanUndo = UndoRedoManager.Instance.CanUndo();
        }

        /// <summary>
        /// Undoes the last action.
        /// </summary>
        private void Undo()
        {
            UndoRedoManager.Instance.Undo();
        }

        /// <summary>
        /// Updates the CanRedo property with a value representing whether there are objects in the
        /// Redoable stack.
        /// </summary>
        private void UpdateCanRedo()
        {
            CanRedo = UndoRedoManager.Instance.CanRedo();
        }

        /// <summary>
        /// Redoes the last action.
        /// </summary>
        private void Redo()
        {
            UndoRedoManager.Instance.Redo();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelDock property.
        /// </summary>
        private void UpdateUIPanelDock()
        {
            this.RaisePropertyChanged(nameof(UIPanelDock));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the UIPanelOrientationDock property.
        /// </summary>
        private void UpdateUIPanelOrientationDock()
        {
            this.RaisePropertyChanged(nameof(UIPanelOrientationDock));
        }

        /// <summary>
        /// Changes the expected orientation layout, if dynamic orientation is enabled,
        /// to the specified orientation.
        /// </summary>
        /// <param name="orientation">
        /// The new expected orientation layout.
        /// </param>
        public void ChangeLayout(Orientation orientation)
        {
            Orientation = orientation;
        }

        /// <summary>
        /// Saves the tracker data to a file at the specified path.
        /// </summary>
        /// <param name="path">
        /// The file path to which the tracker data is to be saved.
        /// </param>
        public void Save(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            SaveData saveData = new SaveData();
            saveData.Save();

            string json = JsonConvert.SerializeObject(saveData);

            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Opens and reads the tracker data from a file at the specified path.
        /// </summary>
        /// <param name="path">
        /// The file path to which the tracker data is to be opened.
        /// </param>
        public void Open(string path)
        {
            string jsonContent = File.ReadAllText(path);
            SaveData saveData = JsonConvert.DeserializeObject<SaveData>(jsonContent);
            saveData.Load();
        }

        /// <summary>
        /// Toggles whether to display all locations on the map.
        /// </summary>
        private void ToggleDisplayAllLocations()
        {
            AppSettings.Instance.DisplayAllLocations = !AppSettings.Instance.DisplayAllLocations;
        }

        /// <summary>
        /// Resets the undo/redo manager, pinned locations, and game data to their starting values.
        /// </summary>
        private void Reset()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                UndoRedoManager.Instance.Reset();
                LocationsPanel.Reset();
                AutoTracker.Instance.Stop();
                BossPlacementDictionary.Instance.Reset();
                ItemDictionary.Instance.Reset();
                LocationDictionary.Instance.Reset();
                PrizePlacementDictionary.Instance.Reset();
                ConnectionCollection.Instance.Clear();
            });
        }

        /// <summary>
        /// Opens a reset dialog window.
        /// </summary>
        private void OpenResetDialog()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                bool? result = await _dialogService.ShowDialog(
                    new MessageBoxDialogVM("Warning",
                    "Resetting the tracker will set all items and locations back to their starting values." +
                    "  This cannot be undone.\n\nDo you wish to proceed?")).ConfigureAwait(false);

                if (result.HasValue && result.Value)
                {
                    Reset();
                }
            });
        }

        /// <summary>
        /// Returns the observable result of the OpenResetDialog method.
        /// </summary>
        /// <returns>
        /// The observable result of the OpenResetDialog method.
        /// </returns>
        private IObservable<Unit> OpenResetDialogAsync()
        {
            return Observable.Start(() => { OpenResetDialog(); });
        }

        /// <summary>
        /// Saves the app settings to a file.
        /// </summary>
        /// <param name="maximized">
        /// A boolean representing whether the window is maximized.
        /// </param>
        /// <param name="bounds">
        /// The boundaries of the window.
        /// </param>
        public void SaveAppSettings(bool maximized, Rect bounds)
        {
            AppSettings.Instance.Maximized = maximized;
            AppSettings.Instance.X = bounds.X;
            AppSettings.Instance.Y = bounds.Y;
            AppSettings.Instance.Width = bounds.Width;
            AppSettings.Instance.Height = bounds.Height;

            string appSettingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "OpenTracker", "OpenTracker.json");

            if (File.Exists(appSettingsPath))
            {
                File.Delete(appSettingsPath);
            }

            string json = JsonConvert.SerializeObject(AppSettings.Instance, Formatting.Indented);
            File.WriteAllText(appSettingsPath, json);
        }

        /// <summary>
        /// Returns the autotracker ViewModel.
        /// </summary>
        /// <returns>
        /// The autotracker ViewModel.
        /// </returns>
        public object GetAutoTrackerViewModel()
        {
            if (_autoTrackerDialog == null)
            {
                _autoTrackerDialog = new AutoTrackerDialogVM();
            }

            return _autoTrackerDialog;
        }

        /// <summary>
        /// Returns a new color select ViewModel.
        /// </summary>
        /// <returns>
        /// A new color select ViewModel.
        /// </returns>
        public object GetColorSelectViewModel()
        {
            return new ColorSelectDialogVM();
        }

        /// <summary>
        /// Returns a new sequence break dialog ViewModel.
        /// </summary>
        /// <returns>
        /// A new color select ViewModel.
        /// </returns>
        public object GetSequenceBreakViewModel()
        {
            return SequenceBreakDialogVMFactory.GetSequenceBreakDialogVM();
        }
    }
}
