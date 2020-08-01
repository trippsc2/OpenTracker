using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using Newtonsoft.Json;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.ViewModels.ColorSelect;
using OpenTracker.ViewModels.MapArea;
using OpenTracker.ViewModels.SequenceBreaks;
using OpenTracker.ViewModels.UIPanels;
using ReactiveUI;
using System;
using System.Collections.Generic;
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
        IDynamicLayout, IOpen, ISave, ICloseHandler, IBounds, ISequenceBreakAccess
    {
        private readonly IDialogService _dialogService;
        private AutoTrackerDialogVM _autoTrackerDialog;

        public bool? Maximized
        {
            get => AppSettings.Instance.Bounds.Maximized;
            set => AppSettings.Instance.Bounds.Maximized = value;
        }
        public double? X
        {
            get => AppSettings.Instance.Bounds.X;
            set => AppSettings.Instance.Bounds.X = value;
        }
        public double? Y
        {
            get => AppSettings.Instance.Bounds.Y;
            set => AppSettings.Instance.Bounds.Y = value;
        }
        public double? Width
        {
            get => AppSettings.Instance.Bounds.Width;
            set => AppSettings.Instance.Bounds.Width = value;
        }
        public double? Height
        {
            get => AppSettings.Instance.Bounds.Height;
            set => AppSettings.Instance.Bounds.Height = value;
        }

        public Dock UIDock =>
            AppSettings.Instance.Layout.CurrentLayoutOrientation switch
            {
                Orientation.Horizontal => AppSettings.Instance.Layout.HorizontalUIPanelPlacement,
                _ => AppSettings.Instance.Layout.VerticalUIPanelPlacement
            };

        public TopMenuVM TopMenu { get; }
        public UIPanelVM UIPanel { get; }
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

            TopMenu = new TopMenuVM(this);
            UIPanel = new UIPanelVM(this);
            MapArea = new MapAreaControlVM(this);

            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
            LoadSequenceBreaks();
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
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.CurrentLayoutOrientation) ||
                e.PropertyName == nameof(LayoutSettings.HorizontalUIPanelPlacement) ||
                e.PropertyName == nameof(LayoutSettings.VerticalUIPanelPlacement))
            {
                this.RaisePropertyChanged(nameof(UIDock));
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
        /// Changes the expected orientation layout, if dynamic orientation is enabled,
        /// to the specified orientation.
        /// </summary>
        /// <param name="orientation">
        /// The new expected orientation layout.
        /// </param>
        public void ChangeLayout(Orientation orientation)
        {
            AppSettings.Instance.Layout.CurrentDynamicOrientation = orientation;
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
            AppSettings.Instance.Tracker.DisplayAllLocations =
                !AppSettings.Instance.Tracker.DisplayAllLocations;
        }

        /// <summary>
        /// Resets the undo/redo manager, pinned locations, and game data to their starting values.
        /// </summary>
        private void Reset()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                UndoRedoManager.Instance.Reset();
                PinnedLocationCollection.Instance.Clear();
                AutoTracker.Instance.Stop();
                BossPlacementDictionary.Instance.Reset();
                LocationDictionary.Instance.Reset();
                PrizePlacementDictionary.Instance.Reset();
                ItemDictionary.Instance.Reset();
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
        /// Saves the app settings to a file.
        /// </summary>
        /// <param name="maximized">
        /// A boolean representing whether the window is maximized.
        /// </param>
        /// <param name="bounds">
        /// The boundaries of the window.
        /// </param>
        private static void SaveAppSettings(bool maximized, Rect bounds)
        {
            AppSettings.Instance.Bounds.Maximized = maximized;
            AppSettings.Instance.Bounds.X = bounds.X;
            AppSettings.Instance.Bounds.Y = bounds.Y;
            AppSettings.Instance.Bounds.Width = bounds.Width;
            AppSettings.Instance.Bounds.Height = bounds.Height;

            string appSettingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "OpenTracker", "OpenTracker.json");

            if (File.Exists(appSettingsPath))
            {
                File.Delete(appSettingsPath);
            }

            string json = JsonConvert.SerializeObject(AppSettings.Instance.Save(), Formatting.Indented);
            File.WriteAllText(appSettingsPath, json);
        }

        /// <summary>
        /// Saves the sequence break settings to a file.
        /// </summary>
        private static void SaveSequenceBreaks()
        {
            string sequenceBreakPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "OpenTracker", "sequencebreak.json");

            if (File.Exists(sequenceBreakPath))
            {
                File.Delete(sequenceBreakPath);
            }

            var sequenceBreakData = SequenceBreakDictionary.Instance.Save();

            string json = JsonConvert.SerializeObject(sequenceBreakData, Formatting.Indented);
            File.WriteAllText(sequenceBreakPath, json);
        }

        /// <summary>
        /// Loads the sequence break settings from a file.
        /// </summary>
        private static void LoadSequenceBreaks()
        {
            string sequenceBreakPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "OpenTracker", "sequencebreak.json");

            if (File.Exists(sequenceBreakPath))
            {
                string jsonContent = File.ReadAllText(sequenceBreakPath);
                Dictionary<SequenceBreakType, SequenceBreakSaveData> sequenceBreaks = JsonConvert
                    .DeserializeObject<Dictionary<SequenceBreakType, SequenceBreakSaveData>>(jsonContent);

                SequenceBreakDictionary.Instance.Load(sequenceBreaks);
            }
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
        /// Handles closing the app.
        /// </summary>
        /// <param name="maximized">
        /// A boolean representing whether the window is maximized.
        /// </param>
        /// <param name="bounds">
        /// The boundaries of the window.
        /// </param>
        public void Close(bool maximized, Rect bounds)
        {
            SaveAppSettings(maximized, bounds);
            SaveSequenceBreaks();
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
