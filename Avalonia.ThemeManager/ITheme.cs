using Avalonia.Styling;

namespace Avalonia.ThemeManager
{
    public interface ITheme
    {
        string Name { get; set; }
        IStyle? Style { get; set; }
        IThemeSelector? Selector { get; set; }

        void ApplyTheme();
    }
}