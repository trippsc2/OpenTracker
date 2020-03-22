using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views
{
    public class AppSettings : UserControl
    {

        public static AvaloniaProperty<bool> EmphasisFontColorPickerOpenProperty =
            AvaloniaProperty.Register<AppSettings, bool>("EmphasisFontColorPickerOpen");
        public bool EmphasisFontColorPickerOpen
        {
            get => GetValue(EmphasisFontColorPickerOpenProperty);
            set => SetValue(EmphasisFontColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityNoneColorPickerOpenProperty =
            AvaloniaProperty.Register<AppSettings, bool>("AccessibilityNoneColorPickerOpen");
        public bool AccessibilityNoneColorPickerOpen
        {
            get => GetValue(AccessibilityNoneColorPickerOpenProperty);
            set => SetValue(AccessibilityNoneColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityPartialColorPickerOpenProperty =
            AvaloniaProperty.Register<AppSettings, bool>("AccessibilityPartialColorPickerOpen");
        public bool AccessibilityPartialColorPickerOpen
        {
            get => GetValue(AccessibilityPartialColorPickerOpenProperty);
            set => SetValue(AccessibilityPartialColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityInspectColorPickerOpenProperty =
            AvaloniaProperty.Register<AppSettings, bool>("AccessibilityInspectColorPickerOpen");
        public bool AccessibilityInspectColorPickerOpen
        {
            get => GetValue(AccessibilityInspectColorPickerOpenProperty);
            set => SetValue(AccessibilityInspectColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilitySequenceBreakColorPickerOpenProperty =
            AvaloniaProperty.Register<AppSettings, bool>("AccessibilitySequenceBreakColorPickerOpen");
        public bool AccessibilitySequenceBreakColorPickerOpen
        {
            get => GetValue(AccessibilitySequenceBreakColorPickerOpenProperty);
            set => SetValue(AccessibilitySequenceBreakColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityNormalColorPickerOpenProperty =
            AvaloniaProperty.Register<AppSettings, bool>("AccessibilityNormalColorPickerOpen");
        public bool AccessibilityNormalColorPickerOpen
        {
            get => GetValue(AccessibilityNormalColorPickerOpenProperty);
            set => SetValue(AccessibilityNormalColorPickerOpenProperty, value);
        }

        public AppSettings()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OpenEmphasisFontColorPickerPopup(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
                EmphasisFontColorPickerOpen = true;
        }
    }
}
