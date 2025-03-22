using Avalonia.Styling;

namespace OpenTracker.Utils.Themes
{
    public interface ITheme
    {
        string Name { get; }
        IStyle Style { get; }

        delegate ITheme Factory(string name, IStyle style);
    }
}