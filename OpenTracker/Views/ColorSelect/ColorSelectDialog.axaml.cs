using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;

namespace OpenTracker.Views.ColorSelect
{
    public class ColorSelectDialog : Window, IDialog
    {
        public static AvaloniaProperty<bool> EmphasisFontColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>(nameof(EmphasisFontColorPickerOpen));
        public bool EmphasisFontColorPickerOpen
        {
            get => (bool)GetValue(EmphasisFontColorPickerOpenProperty);
            set => SetValue(EmphasisFontColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityNoneColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>(nameof(AccessibilityNoneColorPickerOpen));
        public bool AccessibilityNoneColorPickerOpen
        {
            get => (bool)GetValue(AccessibilityNoneColorPickerOpenProperty);
            set => SetValue(AccessibilityNoneColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityPartialColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>(nameof(AccessibilityPartialColorPickerOpen));
        public bool AccessibilityPartialColorPickerOpen
        {
            get => (bool)GetValue(AccessibilityPartialColorPickerOpenProperty);
            set => SetValue(AccessibilityPartialColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityInspectColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>(nameof(AccessibilityInspectColorPickerOpen));
        public bool AccessibilityInspectColorPickerOpen
        {
            get => (bool)GetValue(AccessibilityInspectColorPickerOpenProperty);
            set => SetValue(AccessibilityInspectColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilitySequenceBreakColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>(nameof(AccessibilitySequenceBreakColorPickerOpen));
        public bool AccessibilitySequenceBreakColorPickerOpen
        {
            get => (bool)GetValue(AccessibilitySequenceBreakColorPickerOpenProperty);
            set => SetValue(AccessibilitySequenceBreakColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> AccessibilityNormalColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>(nameof(AccessibilityNormalColorPickerOpen));
        public bool AccessibilityNormalColorPickerOpen
        {
            get => (bool)GetValue(AccessibilityNormalColorPickerOpenProperty);
            set => SetValue(AccessibilityNormalColorPickerOpenProperty, value);
        }

        public static AvaloniaProperty<bool> ConnectorColorPickerOpenProperty =
            AvaloniaProperty.Register<ColorSelectDialog, bool>(nameof(ConnectorColorPickerOpen));
        public bool ConnectorColorPickerOpen
        {
            get => (bool)GetValue(ConnectorColorPickerOpenProperty);
            set => SetValue(ConnectorColorPickerOpenProperty, value);
        }

        public ColorSelectDialog()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
