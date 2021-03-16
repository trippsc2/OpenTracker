using Avalonia.Controls;
using Avalonia.Layout;
using System.ComponentModel;
using ReactiveUI;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This class contains GUI layout settings data.
    /// </summary>
    public class LayoutSettings : ReactiveObject, ILayoutSettings
    {
        private bool _displayMapsCompasses;
        public bool DisplayMapsCompasses
        {
            get => _displayMapsCompasses;
            set => this.RaiseAndSetIfChanged(ref _displayMapsCompasses, value);
        }

        private bool _alwaysDisplayDungeonItems;
        public bool AlwaysDisplayDungeonItems
        {
            get => _alwaysDisplayDungeonItems;
            set => this.RaiseAndSetIfChanged(ref _alwaysDisplayDungeonItems, value);
        }

        private Orientation _currentDynamicOrientation;
        public Orientation CurrentDynamicOrientation
        {
            get => _currentDynamicOrientation;
            set => this.RaiseAndSetIfChanged(ref _currentDynamicOrientation, value);
        }

        private Orientation _currentLayoutOrientation;
        public Orientation CurrentLayoutOrientation
        {
            get => _currentLayoutOrientation;
            private set => this.RaiseAndSetIfChanged(ref _currentLayoutOrientation, value);
        }

        private Orientation _currentMapOrientation;
        public Orientation CurrentMapOrientation
        {
            get => _currentMapOrientation;
            private set => this.RaiseAndSetIfChanged(ref _currentMapOrientation, value);
        }

        private Orientation? _layoutOrientation;
        public Orientation? LayoutOrientation
        {
            get => _layoutOrientation;
            set => this.RaiseAndSetIfChanged(ref _layoutOrientation, value);
        }

        private Orientation? _mapOrientation;
        public Orientation? MapOrientation
        {
            get => _mapOrientation;
            set => this.RaiseAndSetIfChanged(ref _mapOrientation, value);
        }

        private Dock _horizontalUIPanelPlacement = Dock.Bottom;
        public Dock HorizontalUIPanelPlacement
        {
            get => _horizontalUIPanelPlacement;
            set => this.RaiseAndSetIfChanged(ref _horizontalUIPanelPlacement, value);
        }

        private Dock _verticalUIPanelPlacement = Dock.Left;
        public Dock VerticalUIPanelPlacement
        {
            get => _verticalUIPanelPlacement;
            set => this.RaiseAndSetIfChanged(ref _verticalUIPanelPlacement, value);
        }

        private Dock _horizontalItemsPlacement = Dock.Left;
        public Dock HorizontalItemsPlacement
        {
            get => _horizontalItemsPlacement;
            set => this.RaiseAndSetIfChanged(ref _horizontalItemsPlacement, value);
        }

        private Dock _verticalItemsPlacement = Dock.Bottom;
        public Dock VerticalItemsPlacement
        {
            get => _verticalItemsPlacement;
            set => this.RaiseAndSetIfChanged(ref _verticalItemsPlacement, value);
        }

        private double _uiScale = 1.0;
        public double UIScale
        {
            get => _uiScale;
            set => this.RaiseAndSetIfChanged(ref _uiScale, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LayoutSettings()
        {
            PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the CollectionChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CurrentDynamicOrientation):
                    UpdateLayoutOrientation();
                    UpdateMapOrientation();
                    break;
                case nameof(LayoutOrientation):
                    UpdateLayoutOrientation();
                    break;
                case nameof(MapOrientation):
                    UpdateMapOrientation();
                    break;
            }
        }

        /// <summary>
        /// Updates the CurrentLayoutOrientation property.
        /// </summary>
        private void UpdateLayoutOrientation()
        {
            CurrentLayoutOrientation = LayoutOrientation ?? CurrentDynamicOrientation;
        }

        /// <summary>
        /// Updates the CurrentMapOrientation property.
        /// </summary>
        private void UpdateMapOrientation()
        {
            CurrentMapOrientation = MapOrientation ?? CurrentDynamicOrientation;
        }
    }
}
