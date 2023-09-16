using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using Avalonia.Threading;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.ColorSelect;

/// <summary>
/// This class contains the color select control ViewModel data.
/// </summary>
public class ColorSelectControlVM : ViewModelBase, IColorSelectControlVM
{
    private readonly IColorSettings _colorSettings;

    private readonly ColorType _type;
        
    public string Label { get; }

    private SolidColorBrush? _color;
    public SolidColorBrush? Color
    {
        get => _color;
        private set
        {
            if (ReferenceEquals(_color, value))
            {
                return;
            }

            if (!(_color is null))
            {
                _color.PropertyChanged -= OnColorChanged;
            }
                
            this.RaiseAndSetIfChanged(ref _color, value);

            if (!(_color is null))
            {
                _color.PropertyChanged += OnColorChanged;
            }
        }
    }

    private bool _pickerOpen;
    public bool PickerOpen
    {
        get => _pickerOpen;
        set => this.RaiseAndSetIfChanged(ref _pickerOpen, value);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="colorSettings">
    /// The color settings data.
    /// </param>
    /// <param name="type">
    /// The color type.
    /// </param>
    public ColorSelectControlVM(IColorSettings colorSettings, ColorType type)
    {
        _colorSettings = colorSettings;
        _type = type;

        switch (_type)
        {
            case ColorType.EmphasisFont:
            {
                Label = "Emphasis";
                _colorSettings.PropertyChanged += OnColorSettingsChanged;
            }
                break;
            case ColorType.AccessibilityNone:
            {
                Label = "None";
                _colorSettings.AccessibilityColors.PropertyChanged += OnAccessibilityColorsChanged;
            }
                break;
            case ColorType.AccessibilityPartial:
            {
                Label = "Partial";
                _colorSettings.AccessibilityColors.PropertyChanged += OnAccessibilityColorsChanged;
            }
                break;
            case ColorType.AccessibilityInspect:
            {
                Label = "Inspect";
                _colorSettings.AccessibilityColors.PropertyChanged += OnAccessibilityColorsChanged;
            }
                break;
            case ColorType.AccessibilitySequenceBreak:
            {
                Label = "Sequence Break/Possibly Available";
                _colorSettings.AccessibilityColors.PropertyChanged += OnAccessibilityColorsChanged;
            }
                break;
            case ColorType.AccessibilityNormal:
            {
                Label = "Normal";
                _colorSettings.AccessibilityColors.PropertyChanged += OnAccessibilityColorsChanged;
            }
                break;
            case ColorType.Connector:
            {
                Label = "Connector";
                _colorSettings.PropertyChanged += OnColorSettingsChanged;
            }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
            
        UpdateColor();
    }
        
    /// <summary>
    /// Subscribes to the PropertyChanged event on the SolidColorBrush class.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnColorChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        await SetColor();
    }

    /// <summary>
    /// Subscribes to the PropertyChanged event on the IColorSettings interface.
    /// </summary>
    /// <param name="sender">
    /// The sending object of the event.
    /// </param>
    /// <param name="e">
    /// The arguments of the PropertyChanged event.
    /// </param>
    private async void OnColorSettingsChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (_type)
        {
            case ColorType.EmphasisFont:
            {
                if (e.PropertyName != nameof(IColorSettings.EmphasisFontColor))
                {
                    return;
                }

                await UpdateColorAsync();
            }
                break;
            case ColorType.Connector:
            {
                if (e.PropertyName != nameof(IColorSettings.ConnectorColor))
                {
                    return;
                }
                    
                await UpdateColorAsync();
            }
                break;
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
        await UpdateColorAsync();
    }

    /// <summary>
    /// Updates the Color property value from the IColorSettings interface.
    /// </summary>
    private void UpdateColor()
    {
        Color = SolidColorBrush.Parse(_type switch
        {
            ColorType.EmphasisFont => _colorSettings.EmphasisFontColor,
            ColorType.AccessibilityNone => _colorSettings.AccessibilityColors[AccessibilityLevel.None],
            ColorType.AccessibilityPartial => _colorSettings.AccessibilityColors[AccessibilityLevel.Partial],
            ColorType.AccessibilityInspect => _colorSettings.AccessibilityColors[AccessibilityLevel.Inspect],
            ColorType.AccessibilitySequenceBreak =>
                _colorSettings.AccessibilityColors[AccessibilityLevel.SequenceBreak],
            ColorType.AccessibilityNormal => _colorSettings.AccessibilityColors[AccessibilityLevel.Normal],
            ColorType.Connector => _colorSettings.ConnectorColor,
            _ => throw new ArgumentOutOfRangeException()
        });
    }

    /// <summary>
    /// Updates the Color property value from the IColorSettings interface asynchronously.
    /// </summary>
    private async Task UpdateColorAsync()
    {
        await Dispatcher.UIThread.InvokeAsync(UpdateColor);
    }

    /// <summary>
    /// Sets the appropriate IColorSettings interface property from the control.
    /// </summary>
    private async Task SetColor()
    {
        if (Color == null)
        {
            return;
        }
            
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            switch (_type)
            {
                case ColorType.EmphasisFont:
                    _colorSettings.EmphasisFontColor = Color.ToString()!;
                    break;
                case ColorType.AccessibilityNone:
                    _colorSettings.AccessibilityColors[AccessibilityLevel.None] = Color.ToString()!;
                    break;
                case ColorType.AccessibilityPartial:
                    _colorSettings.AccessibilityColors[AccessibilityLevel.Partial] = Color.ToString()!;
                    break;
                case ColorType.AccessibilityInspect:
                    _colorSettings.AccessibilityColors[AccessibilityLevel.Inspect] = Color.ToString()!;
                    break;
                case ColorType.AccessibilitySequenceBreak:
                    _colorSettings.AccessibilityColors[AccessibilityLevel.SequenceBreak] = Color.ToString()!;
                    break;
                case ColorType.AccessibilityNormal:
                    _colorSettings.AccessibilityColors[AccessibilityLevel.Normal] = Color.ToString()!;
                    break;
                case ColorType.Connector:
                    _colorSettings.ConnectorColor = Color.ToString()!;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
    }
}