using System.ComponentModel;

namespace OpenTracker.Models
{
    public class MemoryAddress : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private byte _value;
        public byte Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public MemoryAddress()
        {
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void Reset()
        {
            _value = 0;
        }
    }
}
