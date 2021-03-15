using System.ComponentModel;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains crystal requirement data.
    /// </summary>
    public class CrystalRequirementItem : CappedItem, ICrystalRequirementItem
    {
        private bool _known;
        public bool Known
        {
            get => _known;
            set => this.RaiseAndSetIfChanged(ref _known, value);
        }

        public new delegate CrystalRequirementItem Factory();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="saveLoadManager">
        /// The save/load manager.
        /// </param>
        public CrystalRequirementItem(ISaveLoadManager saveLoadManager)
            : base(saveLoadManager, 0, 7, null)
        {
            PropertyChanged += OnPropertyChanged;
        }
        
        /// <summary>
        /// Subscribes to the PropertyChanged event on this object.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Known))
            {
                this.RaisePropertyChanged(nameof(Current));
            }
        }

        /// <summary>
        /// Resets the item to its starting value.
        /// </summary>
        public override void Reset()
        {
            Known = false;
            base.Reset();
        }
    }
}
