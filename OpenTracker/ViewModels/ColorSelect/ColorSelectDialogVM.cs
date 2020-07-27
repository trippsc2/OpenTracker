using Avalonia;
using Avalonia.Media;
using OpenTracker.Models;
using OpenTracker.Models.AccessibilityLevels;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels.ColorSelect
{
    /// <summary>
    /// This is the ViewModel for the color select dialog window.
    /// </summary>
    public class ColorSelectDialogVM : ViewModelBase
    {
        private SolidColorBrush _emphasisFontColor;
        public SolidColorBrush EmphasisFontColor
        {
            get => _emphasisFontColor;
            set => this.RaiseAndSetIfChanged(ref _emphasisFontColor, value);
        }

        private SolidColorBrush _accessibilityNoneColor;
        public SolidColorBrush AccessibilityNoneColor
        {
            get => _accessibilityNoneColor;
            set => this.RaiseAndSetIfChanged(ref _accessibilityNoneColor, value);
        }

        private SolidColorBrush _accessibilityPartialColor;
        public SolidColorBrush AccessibilityPartialColor
        {
            get => _accessibilityPartialColor;
            set => this.RaiseAndSetIfChanged(ref _accessibilityPartialColor, value);
        }

        private SolidColorBrush _accessibilityInspectColor;
        public SolidColorBrush AccessibilityInspectColor
        {
            get => _accessibilityInspectColor;
            set => this.RaiseAndSetIfChanged(ref _accessibilityInspectColor, value);
        }

        private SolidColorBrush _accessibilitySequenceBreakColor;
        public SolidColorBrush AccessibilitySequenceBreakColor
        {
            get => _accessibilitySequenceBreakColor;
            set => this.RaiseAndSetIfChanged(ref _accessibilitySequenceBreakColor, value);
        }

        private SolidColorBrush _accessibilityNormalColor;
        public SolidColorBrush AccessibilityNormalColor
        {
            get => _accessibilityNormalColor;
            set => this.RaiseAndSetIfChanged(ref _accessibilityNormalColor, value);
        }

        private SolidColorBrush _connectorColor;
        public SolidColorBrush ConnectorColor
        {
            get => _connectorColor;
            set => this.RaiseAndSetIfChanged(ref _connectorColor, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ColorSelectDialogVM()
        {
            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;
            AppSettings.Instance.AccessibilityColors.PropertyChanged += OnAccessibilityColorsChanged;

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
        private void OnColorChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender == EmphasisFontColor)
            {
                SetEmphasisFontColor();
            }

            if (sender == AccessibilityNoneColor)
            {
                SetAccessibilityColor(AccessibilityLevel.None);
            }

            if (sender == AccessibilityPartialColor)
            {
                SetAccessibilityColor(AccessibilityLevel.Partial);
            }

            if (sender == AccessibilityInspectColor)
            {
                SetAccessibilityColor(AccessibilityLevel.Inspect);
            }

            if (sender == AccessibilitySequenceBreakColor)
            {
                SetAccessibilityColor(AccessibilityLevel.SequenceBreak);
            }

            if (sender == AccessibilityNormalColor)
            {
                SetAccessibilityColor(AccessibilityLevel.Normal);
            }

            if (sender == ConnectorColor)
            {
                SetConnectorColor();
            }
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
            if (e.PropertyName == nameof(AppSettings.EmphasisFontColor))
            {
                UpdateEmphasisFontColor();
            }

            if (e.PropertyName == nameof(AppSettings.ConnectorColor))
            {
                UpdateConnectorColor();
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
        private void OnAccessibilityColorsChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibilityColors();
        }

        /// <summary>
        /// Updates the EmphasisFontColor property value from the AppSettings class.
        /// </summary>
        private void UpdateEmphasisFontColor()
        {
            if (EmphasisFontColor != null)
            {
                EmphasisFontColor.PropertyChanged -= OnColorChanged;
            }

            EmphasisFontColor = SolidColorBrush.Parse(AppSettings.Instance.EmphasisFontColor);
            EmphasisFontColor.PropertyChanged += OnColorChanged;
        }

        /// <summary>
        /// Sets EmphasisFontColor value in the AppSettings class.
        /// </summary>
        private void SetEmphasisFontColor()
        {
            AppSettings.Instance.EmphasisFontColor = EmphasisFontColor.Color.ToString();
        }

        /// <summary>
        /// Updates the ConnectorColor property value from the AppSettings class.
        /// </summary>
        private void UpdateConnectorColor()
        {
            if (ConnectorColor != null)
            {
                ConnectorColor.PropertyChanged -= OnColorChanged;
            }

            ConnectorColor = SolidColorBrush.Parse(AppSettings.Instance.ConnectorColor);
            ConnectorColor.PropertyChanged += OnColorChanged;
        }

        /// <summary>
        /// Sets ConnectorColor value in the AppSettings class.
        /// </summary>
        private void SetConnectorColor()
        {
            AppSettings.Instance.ConnectorColor = ConnectorColor.Color.ToString();
        }

        /// <summary>
        /// Updates AccessibilityColors property value from the AppSettings class.
        /// </summary>
        private void UpdateAccessibilityColors()
        {
            if (AccessibilityNoneColor != null)
            {
                AccessibilityNoneColor.PropertyChanged -= OnColorChanged;
            }

            if (AccessibilityInspectColor != null)
            {
                AccessibilityInspectColor.PropertyChanged -= OnColorChanged;
            }

            if (AccessibilityPartialColor != null)
            {
                AccessibilityPartialColor.PropertyChanged -= OnColorChanged;
            }

            if (AccessibilitySequenceBreakColor != null)
            {
                AccessibilitySequenceBreakColor.PropertyChanged -= OnColorChanged;
            }

            if (AccessibilityNormalColor != null)
            {
                AccessibilityNormalColor.PropertyChanged -= OnColorChanged;
            }

            AccessibilityNoneColor = SolidColorBrush
                .Parse(AppSettings.Instance.AccessibilityColors[AccessibilityLevel.None]);
            AccessibilityInspectColor = SolidColorBrush
                .Parse(AppSettings.Instance.AccessibilityColors[AccessibilityLevel.Inspect]);
            AccessibilityPartialColor = SolidColorBrush
                .Parse(AppSettings.Instance.AccessibilityColors[AccessibilityLevel.Partial]);
            AccessibilitySequenceBreakColor = SolidColorBrush
                .Parse(AppSettings.Instance.AccessibilityColors[AccessibilityLevel.SequenceBreak]);
            AccessibilityNormalColor = SolidColorBrush
                .Parse(AppSettings.Instance.AccessibilityColors[AccessibilityLevel.Normal]);

            AccessibilityNoneColor.PropertyChanged += OnColorChanged;
            AccessibilityInspectColor.PropertyChanged += OnColorChanged;
            AccessibilityPartialColor.PropertyChanged += OnColorChanged;
            AccessibilitySequenceBreakColor.PropertyChanged += OnColorChanged;
            AccessibilityNormalColor.PropertyChanged += OnColorChanged;
        }

        /// <summary>
        /// Sets the specified accessibility level in AccessibilityColors property in the AppSettings
        /// class.
        /// </summary>
        /// <param name="accessibility">
        /// The accessibility level to be set.
        /// </param>
        private void SetAccessibilityColor(AccessibilityLevel accessibility)
        {
            SolidColorBrush color = accessibility switch
            {
                AccessibilityLevel.None => AccessibilityNoneColor,
                AccessibilityLevel.Inspect => AccessibilityInspectColor,
                AccessibilityLevel.Partial => AccessibilityPartialColor,
                AccessibilityLevel.SequenceBreak => AccessibilitySequenceBreakColor,
                AccessibilityLevel.Normal => AccessibilityNormalColor,
                _ => null
            };

            if (color == null)
            {
                throw new ArgumentOutOfRangeException(nameof(accessibility));
            }

            AppSettings.Instance.AccessibilityColors[accessibility] = color.ToString();
        }
    }
}