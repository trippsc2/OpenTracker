using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views
{
    public class ColorSelectDialog : Window, IDialog
    {
        public static AvaloniaProperty<bool> EmphasisFontColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>("EmphasisFontColorPickerOpen");
        public bool EmphasisFontColorPickerOpen
        {
            get => GetValue(EmphasisFontColorPickerOpenProperty);
            set => SetValue(EmphasisFontColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityNoneColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>("AccessibilityNoneColorPickerOpen");
        public bool AccessibilityNoneColorPickerOpen
        {
            get => GetValue(AccessibilityNoneColorPickerOpenProperty);
            set => SetValue(AccessibilityNoneColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityPartialColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>("AccessibilityPartialColorPickerOpen");
        public bool AccessibilityPartialColorPickerOpen
        {
            get => GetValue(AccessibilityPartialColorPickerOpenProperty);
            set => SetValue(AccessibilityPartialColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityInspectColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>("AccessibilityInspectColorPickerOpen");
        public bool AccessibilityInspectColorPickerOpen
        {
            get => GetValue(AccessibilityInspectColorPickerOpenProperty);
            set => SetValue(AccessibilityInspectColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilitySequenceBreakColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>("AccessibilitySequenceBreakColorPickerOpen");
        public bool AccessibilitySequenceBreakColorPickerOpen
        {
            get => GetValue(AccessibilitySequenceBreakColorPickerOpenProperty);
            set => SetValue(AccessibilitySequenceBreakColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityNormalColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>("AccessibilityNormalColorPickerOpen");
        public bool AccessibilityNormalColorPickerOpen
        {
            get => GetValue(AccessibilityNormalColorPickerOpenProperty);
            set => SetValue(AccessibilityNormalColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> ConnectorColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>("ConnectorColorPickerOpen");
        public bool ConnectorColorPickerOpen
        {
            get => GetValue(ConnectorColorPickerOpenProperty);
            set => SetValue(ConnectorColorPickerOpenProperty, value);
        }

        public ColorSelectDialog()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            App.Selector.EnableThemes(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
