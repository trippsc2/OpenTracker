using Avalonia;
using Avalonia.Media;
using OpenTracker.Enums;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class MapLocationControlVM : ViewModelBase, IMapLocationControlVM
    {
        private readonly AppSettingsVM _appSettings;
        private readonly LocationPlacement _placement;
        private readonly Game _game;

        private double _canvasX;
        public double CanvasX
        {
            get => _canvasX;
            private set => this.RaiseAndSetIfChanged(ref _canvasX, value);
        }

        private double _canvasY;
        public double CanvasY
        {
            get => _canvasY;
            private set => this.RaiseAndSetIfChanged(ref _canvasY, value);
        }

        private double _size;
        public double Size
        {
            get => _size;
            private set => this.RaiseAndSetIfChanged(ref _size, value);
        }

        private Thickness _borderSize;
        public Thickness BorderSize
        {
            get => _borderSize;
            private set => this.RaiseAndSetIfChanged(ref _borderSize, value);
        }

        private Accessibility _accessibility;
        public Accessibility Accessibility
        {
            get => _accessibility;
            private set => this.RaiseAndSetIfChanged(ref _accessibility, value);
        }

        private IBrush _color;
        public IBrush Color
        {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        private bool _visible;
        public bool Visible
        {
            get => _visible;
            set => this.RaiseAndSetIfChanged(ref _visible, value);
        }

        private void SetSizeAndPosition()
        {
            if (_game.Mode.EntranceShuffle.HasValue &&
                _game.Mode.EntranceShuffle.Value)
            {
                Size = 40.0;
                BorderSize = new Thickness(5);
            }
            else
            {
                Size = 70.0;
                BorderSize = new Thickness(9);
            }

            CanvasX = _placement.X - (Size / 2);
            CanvasY = _placement.Y - (Size / 2);
        }

        public void SetColor()
        {
            Color = _appSettings.AccessibilityColors[_placement.Location.GetAccessibility()];
        }

        public void SetVisibility()
        {
            if (_game.Mode.Validate(_placement.VisibilityMode) && (_appSettings.DisplayAllLocations ||
                (_placement.Location.GetAccessibility() != Accessibility.Cleared &&
                _placement.Location.GetAccessibility() != Accessibility.None)))
                Visible = true;
            else
                Visible = false;
        }

        private void GameModeChanged(object sender, PropertyChangedEventArgs e)
        {
            SetSizeAndPosition();
            SetColor();
            SetVisibility();
        }

        private void ItemChanged(object sender, PropertyChangedEventArgs e)
        {
            SetColor();
            SetVisibility();
        }

        public MapLocationControlVM(AppSettingsVM appSettings, Game game, LocationPlacement coord)
        {
            _appSettings = appSettings;
            _placement = coord;
            _game = game;

            game.Mode.PropertyChanged += GameModeChanged;

            foreach (Item item in game.Items.Values)
                item.PropertyChanged += ItemChanged;

            SetSizeAndPosition();
            Color = _appSettings.AccessibilityColors[Accessibility.Normal];
        }
    }
}
