namespace OpenTracker.ViewModels.ColorSelect;

/// <summary>
/// This interface contains the color select control ViewModel data.
/// </summary>
public interface IColorSelectControlVM
{
    delegate IColorSelectControlVM Factory(ColorType type);
}