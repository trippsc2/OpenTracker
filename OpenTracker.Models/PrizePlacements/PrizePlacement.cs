using OpenTracker.Models.Items;
using OpenTracker.Models.SaveLoad;
using System;
using System.ComponentModel;
using System.Linq;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This is the class for prize placements.
    /// </summary>
    public class PrizePlacement : IPrizePlacement
    {
        private readonly IItem _startingPrize;

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private IItem _prize;
        public IItem Prize
        {
            get => _prize;
            set
            {
                if (_prize != value)
                {
                    OnPropertyChanging(nameof(Prize));
                    _prize = value;
                    OnPropertyChanged(nameof(Prize));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startingPrize">
        /// The starting prize item.
        /// </param>
        public PrizePlacement(IItem startingPrize = null)
        {
            _startingPrize = startingPrize;
            Prize = startingPrize;
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

        /// <summary>
        /// Returns whether the prize can be cycled.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the prize can be cycled.
        /// </returns>
        public bool CanCycle()
        {
            return _startingPrize == null;
        }

        /// <summary>
        /// Cycles the prize.
        /// </summary>
        public void Cycle()
        {
            if (Prize == null)
            {
                Prize = PrizeDictionary.Instance[PrizeType.Crystal];
            }
            else
            {
                PrizeType type = PrizeDictionary.Instance.FirstOrDefault(
                    x => x.Value == Prize).Key;

                if (type == PrizeType.GreenPendant)
                {
                    Prize = null;
                }
                else
                {
                    Prize = PrizeDictionary.Instance[type + 1];
                }
            }
        }

        /// <summary>
        /// Resets the prize placement to its starting values.
        /// </summary>
        public void Reset()
        {
            Prize = _startingPrize;
        }

        /// <summary>
        /// Returns a new prize placement save data instance for this prize placement.
        /// </summary>
        /// <returns>
        /// A new prize placement save data instance.
        /// </returns>
        public PrizePlacementSaveData Save()
        {
            PrizeType? prize;

            if (Prize == null)
            {
                prize = null;
            }
            else
            {
                prize = PrizeDictionary.Instance.FirstOrDefault(x => x.Value == Prize).Key;
            }

            return new PrizePlacementSaveData()
            {
                Prize = prize
            };
        }

        /// <summary>
        /// Loads prize placement save data.
        /// </summary>
        public void Load(PrizePlacementSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            if (saveData.Prize == null)
            {
                Prize = null;
            }
            else
            {
                Prize = PrizeDictionary.Instance[saveData.Prize.Value];
            }
        }
    }
}
