using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace Avalonia.ThemeManager
{
    public sealed class ThemeSelector : ReactiveObject, IThemeSelector
    {
        private ITheme? _selectedTheme;
        private IList<ITheme>? _themes;

        public ITheme? SelectedTheme
        {
            get => _selectedTheme;
            set => this.RaiseAndSetIfChanged(ref _selectedTheme, value);
        }

        public IList<ITheme>? Themes
        {
            get => _themes;
            set => this.RaiseAndSetIfChanged(ref _themes, value);
        }

        public Application App { get; }

        private ThemeSelector(Application app)
        {
            App = app ?? throw new ArgumentNullException(nameof(app));
        }

        public static IThemeSelector Create(string path, Application app)
        {
            return new ThemeSelector(app)
            {
                Themes = new ObservableCollection<ITheme>()
            }.LoadThemes(path);
        }

        private IThemeSelector LoadThemes(string path)
        {
            try
            {
                foreach (var file in Directory.EnumerateFiles(path, "*.xaml"))
                {
                    var theme = LoadTheme(file);
                    
                    _themes?.Add(theme);
                }
            }
            catch
            {
            }

            if (_themes?.Count == 0)
            {
                var light = new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ThemeManager"))
                {
                    Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseLight.xaml?assembly=Avalonia.Themes.Default")
                };

                var dark = new StyleInclude(new Uri("resm:Styles?assembly=Avalonia.ThemeManager"))
                {
                    Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseDark.xaml?assembly=Avalonia.Themes.Default")
                };

                _themes.Add(new Theme() { Name = "Light", Style = light, Selector = this });
                _themes.Add(new Theme() { Name = "Dark", Style = dark, Selector = this });
            }

            _selectedTheme = _themes?.FirstOrDefault();

            if (_selectedTheme?.Style != null)
            {
                App.Styles.Insert(0, _selectedTheme.Style);
            }

            var disposable = this.WhenAnyValue(x => x.SelectedTheme).Where(x => x != null).Subscribe(x =>
            {
                if (x?.Style != null)
                {
                    App.Styles[0] = x.Style;
                }
            });

            return this;
        }

        public ITheme LoadTheme(string file)
        {
            var name = Path.GetFileNameWithoutExtension(file);
            var xaml = File.ReadAllText(file);
            var style = AvaloniaRuntimeXamlLoader.Parse<IStyle>(xaml);

            return new Theme() { Name = name, Style = style, Selector = this };
        }

        public void ApplyTheme(ITheme theme)
        {
            SelectedTheme = theme;
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
                
                var theme = _themes!.FirstOrDefault(x => x.Name == name);

                if (theme != null)
                {
                    SelectedTheme = theme;
                }
            }
            catch
            {
            }
        }

        public void SaveSelectedTheme(string file)
        {
            try
            {
                File.WriteAllText(file, _selectedTheme?.Name);
            }
            catch
            {
            }
        }
    }
}
