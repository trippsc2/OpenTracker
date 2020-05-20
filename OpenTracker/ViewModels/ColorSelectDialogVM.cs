using Avalonia;
using Avalonia.Media;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class ColorSelectDialogVM : ViewModelBase
    {
        private readonly AppSettings _appSettings;

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

        public ColorSelectDialogVM(AppSettings appSettings)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));

            appSettings.PropertyChanged += OnAppSettingsChanged;
            appSettings.AccessibilityColors.PropertyChanged += OnAccessibilityColorsChanged;

            UpdateEmphasisFontColor();
            UpdateAccessibilityColors();
            UpdateConnectorColor();
        }

        private void OnColorChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (sender == EmphasisFontColor)
                SetEmphasisFontColor();

            if (sender == AccessibilityNoneColor)
                SetAccessibilityColor(AccessibilityLevel.None);
            
            if (sender == AccessibilityPartialColor)
                SetAccessibilityColor(AccessibilityLevel.Partial);

            if (sender == AccessibilityInspectColor)
                SetAccessibilityColor(AccessibilityLevel.Inspect);

            if (sender == AccessibilitySequenceBreakColor)
                SetAccessibilityColor(AccessibilityLevel.SequenceBreak);

            if (sender == AccessibilityNormalColor)
                SetAccessibilityColor(AccessibilityLevel.Normal);

            if (sender == ConnectorColor)
                SetConnectorColor();
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.EmphasisFontColor))
                UpdateEmphasisFontColor();

            if (e.PropertyName == nameof(AppSettings.ConnectorColor))
                UpdateConnectorColor();
        }

        private void OnAccessibilityColorsChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibilityColors();
        }

        private void UpdateEmphasisFontColor()
        {
            if (EmphasisFontColor != null)
                EmphasisFontColor.PropertyChanged -= OnColorChanged;

            EmphasisFontColor = SolidColorBrush.Parse(_appSettings.EmphasisFontColor);

            EmphasisFontColor.PropertyChanged += OnColorChanged;
        }

        private void SetEmphasisFontColor()
        {
            _appSettings.EmphasisFontColor = EmphasisFontColor.Color.ToString();
        }

        private void UpdateConnectorColor()
        {
            if (ConnectorColor != null)
                ConnectorColor.PropertyChanged -= OnColorChanged;

            ConnectorColor = SolidColorBrush.Parse(_appSettings.ConnectorColor);

            ConnectorColor.PropertyChanged += OnColorChanged;
        }

        private void SetConnectorColor()
        {
            _appSettings.ConnectorColor = ConnectorColor.Color.ToString();
        }

        private void UpdateAccessibilityColors()
        {
            if (AccessibilityNoneColor != null)
                AccessibilityNoneColor.PropertyChanged -= OnColorChanged;

            if (AccessibilityInspectColor != null)
                AccessibilityInspectColor.PropertyChanged -= OnColorChanged;

            if (AccessibilityPartialColor != null)
                AccessibilityPartialColor.PropertyChanged -= OnColorChanged;

            if (AccessibilitySequenceBreakColor != null)
                AccessibilitySequenceBreakColor.PropertyChanged -= OnColorChanged;

            if (AccessibilityNormalColor != null)
                AccessibilityNormalColor.PropertyChanged -= OnColorChanged;

            AccessibilityNoneColor = SolidColorBrush
                .Parse(_appSettings.AccessibilityColors[AccessibilityLevel.None]);
            AccessibilityInspectColor = SolidColorBrush
                .Parse(_appSettings.AccessibilityColors[AccessibilityLevel.Inspect]);
            AccessibilityPartialColor = SolidColorBrush
                .Parse(_appSettings.AccessibilityColors[AccessibilityLevel.Partial]);
            AccessibilitySequenceBreakColor = SolidColorBrush
                .Parse(_appSettings.AccessibilityColors[AccessibilityLevel.SequenceBreak]);
            AccessibilityNormalColor = SolidColorBrush
                .Parse(_appSettings.AccessibilityColors[AccessibilityLevel.Normal]);

            AccessibilityNoneColor.PropertyChanged += OnColorChanged;
            AccessibilityInspectColor.PropertyChanged += OnColorChanged;
            AccessibilityPartialColor.PropertyChanged += OnColorChanged;
            AccessibilitySequenceBreakColor.PropertyChanged += OnColorChanged;
            AccessibilityNormalColor.PropertyChanged += OnColorChanged;
        }

        private void SetAccessibilityColor(AccessibilityLevel accessibility)
        {
            SolidColorBrush color = null;

            switch (accessibility)
            {
                case AccessibilityLevel.None:
                    color = AccessibilityNoneColor;
                    break;
                case AccessibilityLevel.Inspect:
                    color = AccessibilityInspectColor;
                    break;
                case AccessibilityLevel.Partial:
                    color = AccessibilityPartialColor;
                    break;
                case AccessibilityLevel.SequenceBreak:
                    color = AccessibilitySequenceBreakColor;
                    break;
                case AccessibilityLevel.Normal:
                    color = AccessibilityNormalColor;
                    break;
            }

            if (color == null)
                return;

            _appSettings.AccessibilityColors[accessibility] = color.ToString();
        }
    }
}