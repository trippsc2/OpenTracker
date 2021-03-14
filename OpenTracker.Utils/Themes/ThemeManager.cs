using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using ReactiveUI;

namespace OpenTracker.Utils.Themes
{
    /// <summary>
    /// This class contains the theme manager data.
    /// </summary>
    public class ThemeManager : ReactiveObject, IThemeManager
    {
        private readonly ITheme.Factory _themeFactory;

        public List<ITheme> Themes { get; }

        private ITheme _selectedTheme;
        public ITheme SelectedTheme
        {
            get => _selectedTheme;
            set => this.RaiseAndSetIfChanged(ref _selectedTheme, value);
        }

        public ThemeManager(ITheme.Factory themeFactory, IStyleHost app, string path)
        {
            _themeFactory = themeFactory;

            Themes = LoadThemesFromFolder(path);
            _selectedTheme = Themes[0];
                
            app.Styles.Insert(0, SelectedTheme.Style);

            var _ = this.WhenAnyValue(x => x.SelectedTheme)
                .Where(x => x != null).Subscribe(x =>
                {
                    if (x?.Style != null)
                    {
                        app.Styles[0] = x.Style;
                    }
                });
        }

        private List<ITheme> GetDefaultThemes()
        {
            var dark = new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ThemeManager"))
            {
                Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseDark.xaml?assembly=Avalonia.Themes.Default")
            };

            var light = new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ThemeManager"))
            {
                Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseLight.xaml?assembly=Avalonia.Themes.Default")
            };

            return new List<ITheme>
            {
                _themeFactory("Dark", dark),
                _themeFactory("Light", light)
            };
        }

        private ITheme LoadTheme(string file)
        {
            var name = Path.GetFileNameWithoutExtension(file);
            var xaml = File.ReadAllText(file);
            var style = AvaloniaRuntimeXamlLoader.Parse<IStyle>(xaml);

            return _themeFactory(name, style);
        }

        private List<ITheme> LoadThemesFromFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                return GetDefaultThemes();
            }

            var themes = new List<ITheme>();

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

                if (name == null)
                {
                    return;
                }
                
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
                File.WriteAllText(file, _selectedTheme.Name);
            }
            catch (Exception e)
            {
                Debug.Write(e.ToString());
            }
        }
    }
}