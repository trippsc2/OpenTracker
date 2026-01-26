using System.Collections.Generic;
using Avalonia.Styling;
using ReactiveUI;

namespace OpenTracker.Utils.Themes;

/// <summary>
/// This interface contains the theme manager data.
/// </summary>
public interface IThemeManager : IReactiveObject
{
    List<ITheme> Themes { get; }
    ITheme SelectedTheme { get; set; }
    void LoadSelectedTheme(string file);
    void SaveSelectedTheme(string file);

    delegate IThemeManager Factory(IStyleHost app, string path);
}