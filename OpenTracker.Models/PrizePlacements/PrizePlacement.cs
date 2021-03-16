using System.Linq;
using OpenTracker.Models.Items;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.SaveLoad;
using ReactiveUI;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This class contains prize placement data.
    /// </summary>
    public class PrizePlacement : ReactiveObject, IPrizePlacement
    {
        private readonly IPrizeDictionary _prizes;
        private readonly IItem? _startingPrize;

        private IItem? _prize;
        public IItem? Prize
        {
            get => _prize;
            set => this.RaiseAndSetIfChanged(ref _prize, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prizes">
        /// The prize dictionary.
        /// </param>
        /// <param name="startingPrize">
        /// The starting prize item.
        /// </param>
        public PrizePlacement(IPrizeDictionary prizes, IItem? startingPrize = null)
        {
            _prizes = prizes;
            _startingPrize = startingPrize;

            Prize = startingPrize;
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
                Prize = _prizes[PrizeType.Crystal];
            }
            else
            {
                var type = _prizes.FirstOrDefault(x => x.Value == Prize).Key;
                Prize = type == PrizeType.GreenPendant ? null : _prizes[type + 1];
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
                prize = _prizes.FirstOrDefault(x => x.Value == Prize).Key;
            }

            return new PrizePlacementSaveData()
            {
                Prize = prize
            };
        }

        /// <summary>
        /// Loads prize placement save data.
        /// </summary>
        public void Load(PrizePlacementSaveData? saveData)
        {
            if (saveData is null)
            {
                return;
            }

            Prize = saveData.Prize == null ? null : _prizes[saveData.Prize.Value];
        }
    }
}
