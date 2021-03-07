using System.Collections.Generic;

namespace Avalonia.ThemeManager
{
    public interface IThemeSelector
    {
        Application App { get; }
        ITheme? SelectedTheme { get; set; }
        IList<ITheme>? Themes { get; set; }

        void ApplyTheme(ITheme theme);
        void LoadSelectedTheme(string file);
        ITheme LoadTheme(string file);
        void SaveSelectedTheme(string file);
    }
}