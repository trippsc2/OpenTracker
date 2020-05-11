using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;
using System.Threading.Tasks;

namespace OpenTracker.Views
{
    public class AutoTrackerDialog : Window
    {
        private ISave ViewModelSave => DataContext as ISave;

        public AutoTrackerDialog()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public async Task Save()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "Text", Extensions = { "txt" } });

            string path = await dialog.ShowAsync(this).ConfigureAwait(false);

            ViewModelSave.Save(path);
        }
    }
}
