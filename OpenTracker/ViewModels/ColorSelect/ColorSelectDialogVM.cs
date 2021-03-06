using Avalonia;
using Avalonia.Media;
using Avalonia.Threading;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Settings;
using OpenTracker.Utils.Dialog;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OpenTracker.ViewModels.ColorSelect
{
    /// <summary>
    /// This class contains the color select dialog window ViewModel data.
    /// </summary>
    public class ColorSelectDialogVM : DialogViewModelBase, IColorSelectDialogVM
    {
        private SolidColorBrush? _emphasisFontColor;
        public SolidColorBrush? EmphasisFontColor
        {
            get => _emphasisFontColor;
            set
            {
                if (!(_emphasisFontColor is null))
                {
                    _emphasisFontColor.PropertyChanged -= OnColorChanged;
                }
                
                this.RaiseAndSetIfChanged(ref _emphasisFontColor, value);

                if (!(_emphasisFontColor is null))
                {
                    _emphasisFontColor.PropertyChanged += OnColorChanged;
                }
            }
        }

        private SolidColorBrush? _accessibilityNoneColor;
        public SolidColorBrush? AccessibilityNoneColor
        {
            get => _accessibilityNoneColor;
            set
            {
                if (!(_accessibilityNoneColor is null))
                {
                    _accessibilityNoneColor.PropertyChanged -= OnColorChanged;
                }
                
                this.RaiseAndSetIfChanged(ref _accessibilityNoneColor, value);

                if (!(_accessibilityNoneColor is null))
                {
                    _accessibilityNoneColor.PropertyChanged += OnColorChanged;
                }
            }
        }

        private SolidColorBrush? _accessibilityPartialColor;
        public SolidColorBrush? AccessibilityPartialColor
        {
            get => _accessibilityPartialColor;
            set
            {
                if (!(_accessibilityPartialColor is null))
                {
                    _accessibilityPartialColor.PropertyChanged -= OnColorChanged;
                }
                
                this.RaiseAndSetIfChanged(ref _accessibilityPartialColor, value);

                if (!(_accessibilityPartialColor is null))
                {
                    _accessibilityPartialColor.PropertyChanged += OnColorChanged;
                }
            }
        }

        private SolidColorBrush? _accessibilityInspectColor;
        public SolidColorBrush? AccessibilityInspectColor
        {
            get => _accessibilityInspectColor;
            set
            {
                if (!(_accessibilityInspectColor is null))
                {
                    _accessibilityInspectColor.PropertyChanged -= OnColorChanged;
                }
                
                this.RaiseAndSetIfChanged(ref _accessibilityInspectColor, value);
                
                if (!(_accessibilityInspectColor is null))
                {
                    _accessibilityInspectColor.PropertyChanged += OnColorChanged;
                }
            }
        }

        private SolidColorBrush? _accessibilitySequenceBreakColor;
        public SolidColorBrush? AccessibilitySequenceBreakColor
        {
            get => _accessibilitySequenceBreakColor;
            set
            {
                if (!(_accessibilitySequenceBreakColor is null))
                {
                    _accessibilitySequenceBreakColor.PropertyChanged -= OnColorChanged;
                }
                
                this.RaiseAndSetIfChanged(ref _accessibilitySequenceBreakColor, value);
                
                if (!(_accessibilitySequenceBreakColor is null))
                {
                    _accessibilitySequenceBreakColor.PropertyChanged += OnColorChanged;
                }
            }
        }

        private SolidColorBrush? _accessibilityNormalColor;
        public SolidColorBrush? AccessibilityNormalColor
        {
            get => _accessibilityNormalColor;
            set
            {
                if (!(_accessibilityNormalColor is null))
                {
                    _accessibilityNormalColor.PropertyChanged -= OnColorChanged;
                }
                
                this.RaiseAndSetIfChanged(ref _accessibilityNormalColor, value);
                
                if (!(_accessibilityNormalColor is null))
                {
                    _accessibilityNormalColor.PropertyChanged += OnColorChanged;
                }
            }
        }

        private SolidColorBrush? _connectorColor;
        public SolidColorBrush? ConnectorColor
        {
            get => _connectorColor;
            set
            {
                if (!(_connectorColor is null))
                {
                    _connectorColor.PropertyChanged -= OnColorChanged;
                }
                
                this.RaiseAndSetIfChanged(ref _connectorColor, value);

                if (!(_connectorColor is null))
                {
                    _connectorColor.PropertyChanged += OnColorChanged;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ColorSelectDialogVM()
        {
            _colorSettings = colorSettings;

            _colorSettings.PropertyChanged += OnColorSettingsChanged;
            _colorSettings.AccessibilityColors.PropertyChanged += OnAccessibilityColorsChanged;

            UpdateEmphasisFontColor();
            UpdateAccessibilityColors();
            UpdateConnectorColor();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the SolidColorBrush classes.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnColorChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender == EmphasisFontColor)
            {
                await SetEmphasisFontColorAsync();
            }

            if (sender == AccessibilityNoneColor)
            {
                await SetAccessibilityColorAsync(AccessibilityLevel.None);
            }

            if (sender == AccessibilityPartialColor)
            {
                await SetAccessibilityColorAsync(AccessibilityLevel.Partial);
            }

            if (sender == AccessibilityInspectColor)
            {
                await SetAccessibilityColorAsync(AccessibilityLevel.Inspect);
            }

            if (sender == AccessibilitySequenceBreakColor)
            {
                await SetAccessibilityColorAsync(AccessibilityLevel.SequenceBreak);
            }

            if (sender == AccessibilityNormalColor)
            {
                await SetAccessibilityColorAsync(AccessibilityLevel.Normal);
            }

            if (sender == ConnectorColor)
            {
                await SetConnectorColorAsync();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ColorSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnColorSettingsChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ColorSettings.EmphasisFontColor))
            {
                await UpdateEmphasisFontColorAsync();
            }

            if (e.PropertyName == nameof(ColorSettings.ConnectorColor))
            {
                await UpdateConnectorColorAsync();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ObservableCollection class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnAccessibilityColorsChanged(object? sender, PropertyChangedEventArgs e)
        {
            await UpdateAccessibilityColorsAsync();
        }

        /// <summary>
        /// Updates the EmphasisFontColor property value from the IAppSettings interface.
        /// </summary>
        private void UpdateEmphasisFontColor()
        {
            EmphasisFontColor = SolidColorBrush.Parse(_colorSettings.EmphasisFontColor);
        }

        /// <summary>
        /// Updates the EmphasisFontColor property value from the IAppSettings interface asynchronously.
        /// </summary>
        private async Task UpdateEmphasisFontColorAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(UpdateEmphasisFontColor);
        }

        /// <summary>
        /// Sets EmphasisFontColor value in the IAppSettings interface.
        /// </summary>
        private void SetEmphasisFontColor()
        {
            if (EmphasisFontColor == null)
            {
                return;
            }

            _colorSettings.EmphasisFontColor = EmphasisFontColor.Color.ToString();
        }

        /// <summary>
        /// Sets EmphasisFontColor value in the IAppSettings interface asynchronously.
        /// </summary>
        private async Task SetEmphasisFontColorAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(SetEmphasisFontColor);
        }

        /// <summary>
        /// Updates the ConnectorColor property value from the IAppSettings interface.
        /// </summary>
        private void UpdateConnectorColor()
        {
            ConnectorColor = SolidColorBrush.Parse(_colorSettings.ConnectorColor);
        }

        /// <summary>
        /// Updates the ConnectorColor property value from the IAppSettings interface asynchronously.
        /// </summary>
        private async Task UpdateConnectorColorAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(UpdateConnectorColor);
        }

        /// <summary>
        /// Sets ConnectorColor value in the IAppSettings interface.
        /// </summary>
        private void SetConnectorColor()
        {
            if (ConnectorColor == null)
            {
                return;
            }

            _colorSettings.ConnectorColor = ConnectorColor.Color.ToString();
        }

        /// <summary>
        /// Sets ConnectorColor value in the IAppSettings interface asynchronously.
        /// </summary>
        private async Task SetConnectorColorAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(SetConnectorColor);
        }

        /// <summary>
        /// Updates AccessibilityColors property value from the IAppSettings interface.
        /// </summary>
        private void UpdateAccessibilityColors()
        {
            AccessibilityNoneColor = SolidColorBrush.Parse(_colorSettings.AccessibilityColors[AccessibilityLevel.None]);
            AccessibilityInspectColor = SolidColorBrush.Parse(
                _colorSettings.AccessibilityColors[AccessibilityLevel.Inspect]);
            AccessibilityPartialColor = SolidColorBrush.Parse(
                _colorSettings.AccessibilityColors[AccessibilityLevel.Partial]);
            AccessibilitySequenceBreakColor = SolidColorBrush.Parse(
                _colorSettings.AccessibilityColors[AccessibilityLevel.SequenceBreak]);
            AccessibilityNormalColor = SolidColorBrush.Parse(
                _colorSettings.AccessibilityColors[AccessibilityLevel.Normal]);
        }

        /// <summary>
        /// Updates AccessibilityColors property value from the IAppSettings interface asynchronously.
        /// </summary>
        private async Task UpdateAccessibilityColorsAsync()
        {
            await Dispatcher.UIThread.InvokeAsync(UpdateAccessibilityColors);
        }

        /// <summary>
        /// Sets the specified accessibility level in AccessibilityColors property in the IAppSettings interface.
        /// </summary>
        /// <param name="accessibility">
        /// The accessibility level to be set.
        /// </param>
        private void SetAccessibilityColor(AccessibilityLevel accessibility)
        {
            SolidColorBrush? color = accessibility switch
            {
                AccessibilityLevel.None => AccessibilityNoneColor,
                AccessibilityLevel.Inspect => AccessibilityInspectColor,
                AccessibilityLevel.Partial => AccessibilityPartialColor,
                AccessibilityLevel.SequenceBreak => AccessibilitySequenceBreakColor,
                AccessibilityLevel.Normal => AccessibilityNormalColor,
                _ => throw new ArgumentOutOfRangeException(nameof(accessibility))
            };

            if (color is null)
            {
                return;
            }

            _colorSettings.AccessibilityColors[accessibility] = color.ToString();
        }

        /// <summary>
        /// Sets the specified accessibility level in AccessibilityColors property in the IAppSettings interface
        /// asynchronously.
        /// </summary>
        /// <param name="accessibility">
        /// The accessibility level to be set.
        /// </param>
        private async Task SetAccessibilityColorAsync(AccessibilityLevel accessibility)
        {
            await Dispatcher.UIThread.InvokeAsync(() => SetAccessibilityColor(accessibility));
        }
    }
}