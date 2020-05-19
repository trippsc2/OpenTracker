using Avalonia;
using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
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
                WorldState worldState;

                if (_game.Mode.WorldState == WorldState.Inverted)
                    worldState = WorldState.Inverted;
                else
                    worldState = WorldState.StandardOpen;

                return "avares://OpenTracker/Assets/Images/Maps/" +
                    worldState.ToString().ToLowerInvariant() + "_" +
                    _iD.ToString().ToLowerInvariant() + ".png";
            }
        }

        public MapControlVM(Game game, MainWindowVM mainWindow, MapID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            _iD = iD;

            _game.Mode.PropertyChanged += OnModeChanged;
            _mainWindow.PropertyChanged += OnMainWindowChanged;
        }

        private void OnMainWindowChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowVM.MapPanelOrientation))
                this.RaisePropertyChanged(nameof(MapMargin));
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState))
                this.RaisePropertyChanged(nameof(ImageSource));
        }
    }
}
