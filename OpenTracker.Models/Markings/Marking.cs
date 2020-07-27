using System.ComponentModel;

namespace OpenTracker.Models.Markings
{
    public class Marking : IMarking
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private MarkType? _mark;
        public MarkType? Mark
        {
            get => _mark;
            set
            {
                if (_mark != value)
                {
                    OnPropertyChanging(nameof(Mark));
                    _mark = value;
                    OnPropertyChanged(nameof(Mark));
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanging event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changing property.
        /// </param>
        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
