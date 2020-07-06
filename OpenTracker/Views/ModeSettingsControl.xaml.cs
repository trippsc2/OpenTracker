using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views
{
    public class ModeSettingsControl : UserControl
    {
        public static AvaloniaProperty<bool> ModeSettingsPopupOpenProperty =
            AvaloniaProperty.Register<MainWindow, bool>(nameof(ModeSettingsPopupOpen));
        public bool ModeSettingsPopupOpen
        {
            get => GetValue(ModeSettingsPopupOpenProperty);
            set => SetValue(ModeSettingsPopupOpenProperty, value);
        }

        public ModeSettingsControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OpenModeSettingsPopup(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                ModeSettingsPopupOpen = true;
            }
        }
    }
}
