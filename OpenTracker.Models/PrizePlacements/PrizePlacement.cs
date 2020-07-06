using OpenTracker.Models.Items;
using System.ComponentModel;

namespace OpenTracker.Models.PrizePlacements
{
    public class PrizePlacement : IPrizePlacement
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IItem _prize;
        public IItem Prize
        {
            get => _prize;
            set
            {
                if (_prize != value)
                {
                    _prize = value;
                    OnPropertyChanged(nameof(Prize));
                }
            }
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

        /// <summary>
        /// Resets the prize placement to its starting values.
        /// </summary>
        public void Reset()
        {
            Prize = null;
        }
    }
}
