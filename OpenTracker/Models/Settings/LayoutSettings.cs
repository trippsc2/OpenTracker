using Avalonia.Controls;
using Avalonia.Layout;
using System.ComponentModel;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This is the class containing tracker GUI layout settings.
    /// </summary>
    public class LayoutSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _displayMapsCompasses;
        public bool DisplayMapsCompasses
        {
            get => _displayMapsCompasses;
            set
            {
                if (_displayMapsCompasses != value)
                {
                    _displayMapsCompasses = value;
                    OnPropertyChanged(nameof(DisplayMapsCompasses));
                }
            }
        }

        private bool _alwaysDisplayDungeonItems;
        public bool AlwaysDisplayDungeonItems
        {
            get => _alwaysDisplayDungeonItems;
            set
            {
                if (_alwaysDisplayDungeonItems != value)
                {
                    _alwaysDisplayDungeonItems = value;
                    OnPropertyChanged(nameof(AlwaysDisplayDungeonItems));
                }
            }
        }

        private Orientation _currentDynamicOrientation;
        public Orientation CurrentDynamicOrientation
        {
            get => _currentDynamicOrientation;
            set
            {
                if (_currentDynamicOrientation != value)
                {
                    _currentDynamicOrientation = value;
                    OnPropertyChanged(nameof(CurrentDynamicOrientation));
                }
            }
        }

        private Orientation _currentLayoutOrientation;
        public Orientation CurrentLayoutOrientation
        {
            get => _currentLayoutOrientation;
            private set
            {
                if (_currentLayoutOrientation != value)
                {
                    _currentLayoutOrientation = value;
                    OnPropertyChanged(nameof(CurrentLayoutOrientation));
                }
            }
        }

        private Orientation _currentMapOrientation;
        public Orientation CurrentMapOrientation
        {
            get => _currentMapOrientation;
            private set
            {
                if (_currentMapOrientation != value)
                {
                    _currentMapOrientation = value;
                    OnPropertyChanged(nameof(CurrentMapOrientation));
                }
            }
        }

        private Orientation? _layoutOrientation;
        public Orientation? LayoutOrientation
        {
            get => _layoutOrientation;
            set
            {
                if (_layoutOrientation != value)
                {
                    _layoutOrientation = value;
                    OnPropertyChanged(nameof(LayoutOrientation));
                }
            }
        }

        private Orientation? _mapOrientation;
        public Orientation? MapOrientation
        {
            get => _mapOrientation;
            set
            {
                if (_mapOrientation != value)
                {
                    _mapOrientation = value;
                    OnPropertyChanged(nameof(MapOrientation));
                }
            }
        }

        private Dock _horizontalUIPanelPlacement = Dock.Bottom;
        public Dock HorizontalUIPanelPlacement
        {
            get => _horizontalUIPanelPlacement;
            set
            {
                if (_horizontalUIPanelPlacement != value)
                {
                    _horizontalUIPanelPlacement = value;
                    OnPropertyChanged(nameof(HorizontalUIPanelPlacement));
                }
            }
        }

        private Dock _verticalUIPanelPlacement = Dock.Left;
        public Dock VerticalUIPanelPlacement
        {
            get => _verticalUIPanelPlacement;
            set
            {
                if (_verticalUIPanelPlacement != value)
                {
                    _verticalUIPanelPlacement = value;
                    OnPropertyChanged(nameof(VerticalUIPanelPlacement));
                }
            }
        }

        private Dock _horizontalItemsPlacement = Dock.Left;
        public Dock HorizontalItemsPlacement
        {
            get => _horizontalItemsPlacement;
            set
            {
                if (_horizontalItemsPlacement != value)
                {
                    _horizontalItemsPlacement = value;
                    OnPropertyChanged(nameof(HorizontalItemsPlacement));
                }
            }
        }

        private Dock _verticalItemsPlacement = Dock.Bottom;
        public Dock VerticalItemsPlacement
        {
            get => _verticalItemsPlacement;
            set
            {
                if (_verticalItemsPlacement != value)
                {
                    _verticalItemsPlacement = value;
                    OnPropertyChanged(nameof(VerticalItemsPlacement));
                }
            }
        }

        private double _uiScale = 1.0;
        public double UIScale
        {
            get => _uiScale;
            set
            {
                if (_uiScale != value)
                {
                    _uiScale = value;
                    OnPropertyChanged(nameof(UIScale));
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(CurrentDynamicOrientation))
            {
                UpdateLayoutOrientation();
                UpdateMapOrientation();
            }

            if (propertyName == nameof(LayoutOrientation))
            {
                UpdateLayoutOrientation();
            }

            if (propertyName == nameof(MapOrientation))
            {
                UpdateMapOrientation();
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
