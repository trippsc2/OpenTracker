using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Styling;

namespace OpenTracker.Utils.Themes;

/// <summary>
/// This interface contains the theme manager data.
/// </summary>
public interface IThemeManager : INotifyPropertyChanged
{
    List<Theme> Themes { get; }
    Theme SelectedTheme { get; set; }
    void LoadSelectedTheme(string file);
    void SaveSelectedTheme(string file);

    delegate IThemeManager Factory(IStyleHost app, string path);
}