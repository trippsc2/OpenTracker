using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using OpenTracker.Utils.Autofac;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Utils.Themes;

/// <summary>
/// This class contains the theme manager data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ThemeManager : ReactiveObject, IThemeManager
{
    public List<Theme> Themes { get; }
    
    [Reactive]
    public Theme SelectedTheme { get; set; }

    public ThemeManager(IStyleHost app, string path)
    {
        Themes = LoadThemesFromFolder(path);
        SelectedTheme = Themes[0];
                
        app.Styles.Insert(0, SelectedTheme.Style);

        var _ = this.WhenAnyValue(x => x.SelectedTheme)
            .Subscribe(x => { app.Styles[0] = x.Style; });
    }

    private static List<Theme> GetDefaultThemes()
    {
        var dark = new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ThemeManager"))
        {
            Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseDark.xaml?assembly=Avalonia.Themes.Default")
        };

        var light = new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ThemeManager"))
        {
            Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseLight.xaml?assembly=Avalonia.Themes.Default")
        };

        return new List<Theme>
        {
            new() {Name = "Dark", Style = dark},
            new() {Name = "Light", Style = light}
        };
    }

    private static Theme LoadTheme(string file)
    {
        var name = Path.GetFileNameWithoutExtension(file);
        var xaml = File.ReadAllText(file);
        var style = AvaloniaRuntimeXamlLoader.Parse<IStyle>(xaml);

        return new Theme {Name = name, Style = style};
    }

    private static List<Theme> LoadThemesFromFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            return GetDefaultThemes();
        }

        var themes = new List<Theme>();

        try
        {
            foreach (var file in Directory.EnumerateFiles(path, "*.xaml"))
            {
                var theme = LoadTheme(file);

                if (theme.Name == "Default")
                {
                    themes.Insert(0, theme);
                    continue;
                }
                    
                themes.Add(theme);
            }
        }
        catch (Exception e)
        {
            Debug.Write(e.ToString());
        }
            
        return themes.Count == 0 ? GetDefaultThemes() : themes;
    }
        
    public void LoadSelectedTheme(string file)
    {
        try
        {
            if (!File.Exists(file))
            {
                return;
            }
                
            var name = File.ReadAllText(file);

            var theme = Themes.FirstOrDefault(x => x.Name == name);

            if (theme is null)
            {
                return;
            }
                
            SelectedTheme = theme;
        }
        catch (Exception e)
        {
            Debug.Write(e.ToString());
        }
    }

    public void SaveSelectedTheme(string file)
    {
        try
        {
            File.WriteAllText(file, SelectedTheme.Name);
        }
        catch (Exception e)
        {
            Debug.Write(e.ToString());
        }
    }
}