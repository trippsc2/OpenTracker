using Avalonia;
using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model of the map controls.
    /// </summary>
    public class MapControlVM : ViewModelBase
    {
        private readonly Game _game;
        private readonly MainWindowVM _mainWindow;
        private readonly MapID _iD;

        public Thickness MapMargin
        {
            get
            {
                return _mainWindow.MapPanelOrientation switch
                {
                    Orientation.Horizontal => new Thickness(10, 20),
                    _ => new Thickness(20, 10),
                };
            }
        }

        public string ImageSource 
        {
            get
            {
                WorldState worldState = _game.Mode.WorldState == WorldState.Inverted ?
                    WorldState.Inverted : WorldState.StandardOpen;

                return $"avares://OpenTracker/Assets/Images/Maps/" +
                    $"{ worldState.ToString().ToLowerInvariant() }" +
                    $"_{ _iD.ToString().ToLowerInvariant() }.png";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="mainWindow">
        /// The view-model of the main window.
        /// </param>
        /// <param name="iD">
        /// The map identity.
        /// </param>
        public MapControlVM(Game game, MainWindowVM mainWindow, MapID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            _iD = iD;

            _game.Mode.PropertyChanged += OnModeChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the MainWindowVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.MapPanelOrientation))
            {
                this.RaisePropertyChanged(nameof(MapMargin));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }
    }
}
