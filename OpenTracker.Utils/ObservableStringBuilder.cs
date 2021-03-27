using System.ComponentModel;
using System.Text;

namespace OpenTracker.Utils
{
    public class ObservableStringBuilder : INotifyPropertyChanged
    {
        private readonly StringBuilder _stringBuilder = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Text =>
            _stringBuilder.ToString();

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Append(string text)
        {
            _stringBuilder.Append(text);
            OnPropertyChanged(nameof(Text));
        }

        public void AppendLine(string text)
        {
            _stringBuilder.AppendLine(text);
            OnPropertyChanged(nameof(Text));
        }

        public void Clear()
        {
            _stringBuilder.Clear();
            OnPropertyChanged(nameof(Text));
        }
    }
}
