using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using Newtonsoft.Json;
using OpenTracker.Interfaces;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Models.Settings;
using OpenTracker.ViewModels.ColorSelect;
using OpenTracker.ViewModels.Maps;
using OpenTracker.ViewModels.SequenceBreaks;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Text;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the main window.
    /// </summary>
    public class MainWindowVM : ViewModelBase, IAutoTrackerAccess, IBoundsData, ICloseHandler,
        IColorSelectAccess, IDynamicLayout, IOpenData, ISaveData, ISequenceBreakAccess
    {
        public static string Title
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("OpenTracker - ");

                if (SaveLoadManager.Instance.CurrentFilePath == null)
                {
                    sb.Append("Untitled");
                }
                else
                {
                    sb.Append(SaveLoadManager.Instance.CurrentFilePath);
                }

                if (SaveLoadManager.Instance.Unsaved)
                {
                    sb.Append('*');
                }

                return sb.ToString();
            }
        }

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

        public string CurrentFilePath =>
            SaveLoadManager.Instance.CurrentFilePath;

        public static Dock UIDock =>
            AppSettings.Instance.Layout.CurrentLayoutOrientation switch
            {
                Orientation.Horizontal => AppSettings.Instance.Layout.HorizontalUIPanelPlacement,
                _ => AppSettings.Instance.Layout.VerticalUIPanelPlacement
            };

        public Screens Screens { get; set; }

        public AutoTrackerDialogVM AutoTrackerDialog { get; } =
            new AutoTrackerDialogVM();
        public TopMenuVM TopMenu { get; } =
            new TopMenuVM();
        public UIPanelVM UIPanel { get; } =
            new UIPanelVM();
        public MapAreaVM MapArea { get; } =
            new MapAreaVM();
        public StatusBarVM StatusBar { get; } =
            new StatusBarVM();

        private bool _aboutPageOpen;
        public bool AboutPageOpen
        {
            get => _aboutPageOpen;
            set => this.RaiseAndSetIfChanged(ref _aboutPageOpen, value);
        }

        public ReactiveCommand<Unit, Unit> OpenResetDialogCommand =>
            TopMenu.OpenResetDialogCommand;
        public ReactiveCommand<Unit, Unit> UndoCommand =>
            TopMenu.UndoCommand;
        public ReactiveCommand<Unit, Unit> RedoCommand =>
            TopMenu.RedoCommand;
        public ReactiveCommand<Unit, Unit> ToggleDisplayAllLocationsCommand =>
            TopMenu.ToggleDisplayAllLocationsCommand;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowVM()
        {
            SaveLoadManager.Instance.PropertyChanged += OnSaveLoadManagerChanged;
            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
            LoadSequenceBreaks();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the SaveLoadManager class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSaveLoadManagerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SaveLoadManager.CurrentFilePath) ||
                e.PropertyName == nameof(SaveLoadManager.Unsaved))
            {
                this.RaisePropertyChanged(nameof(Title));
            }
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

        private static void OpenErrorBox(string message)
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                bool? result = await App.DialogService.ShowDialog(
                    new ErrorBoxDialogVM("Error", message))
                    .ConfigureAwait(false);
            });
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
        public void Save(string path = null)
        {
            try
            {
                SaveLoadManager.Save(path);
            }
            catch (Exception ex)
            {
                string message;

                switch (ex)
                {
                    case UnauthorizedAccessException uaex:
                        {
                            message = "Unable to save to the selected directory.  Check the file permissions and try again.";
                        }
                        break;
                    default:
                        {
                            message = ex.Message;
                        }
                        break;
                }

                OpenErrorBox(message);
            }
        }

        /// <summary>
        /// Opens and reads the tracker data from a file at the specified path.
        /// </summary>
        /// <param name="path">
        /// The file path to which the tracker data is to be opened.
        /// </param>
        public void Open(string path)
        {
            try
            {
                SaveLoadManager.Open(path);
            }
            catch (Exception ex)
            {
                string message;

                switch (ex)
                {
                    case JsonReaderException jex:
                        {
                            message = "The selected file is not a valid JSON file.";
                        }
                        break;
                    case UnauthorizedAccessException uaex:
                        {
                            message = "The file cannot be read.  Check the permissions on the selected file.";
                        }
                        break;
                    default:
                        {
                            message = ex.Message;
                        }
                        break;
                }

                OpenErrorBox(message);
            }
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
        private static void SaveAppSettings(bool maximized, Rect bounds, PixelPoint pixelPoint)
        {
            AppSettings.Instance.Bounds.Maximized = maximized;
            AppSettings.Instance.Bounds.X = pixelPoint.X;
            AppSettings.Instance.Bounds.Y = pixelPoint.Y;
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
        /// Handles closing the app.
        /// </summary>
        /// <param name="maximized">
        /// A boolean representing whether the window is maximized.
        /// </param>
        /// <param name="bounds">
        /// The boundaries of the window.
        /// </param>
        public void Close(bool maximized, Rect bounds, PixelPoint pixelPoint)
        {
            SaveAppSettings(maximized, bounds, pixelPoint);
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
            return AutoTrackerDialog;
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
