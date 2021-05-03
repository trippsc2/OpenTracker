using System;
using System.Globalization;
using System.Linq;
using OpenTracker.Models.Items;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Prize;
using ReactiveUI;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    ///     This class contains prize placement data.
    /// </summary>
    public class PrizePlacement : ReactiveObject, IPrizePlacement
    {
        private readonly IPrizeDictionary _prizes;

        private readonly IChangePrize.Factory _changePrizeFactory;
        
        private readonly IItem? _startingPrize;

        private IItem? _prize;
        public IItem? Prize
        {
            get => _prize;
            private set => this.RaiseAndSetIfChanged(ref _prize, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="prizes">
        ///     The prize dictionary.
        /// </param>
        /// <param name="changePrizeFactory">
        ///     An Autofac factory for creating undoable actions to change the prize.
        /// </param>
        /// <param name="startingPrize">
        ///     The starting prize item.
        /// </param>
        public PrizePlacement(
            IPrizeDictionary prizes, IChangePrize.Factory changePrizeFactory, IItem? startingPrize = null)
        {
            _prizes = prizes;
            _changePrizeFactory = changePrizeFactory;
            _startingPrize = startingPrize;

            Prize = startingPrize;
        }

        public bool CanCycle()
        {
            return _startingPrize == null;
        }

        public void Cycle(bool reverse = false)
        {
            if (Prize is null)
            {
                if (reverse)
                {
                    Prize = _prizes[PrizeType.GreenPendant];
                    return;
                }
                
                Prize = _prizes[PrizeType.Crystal];
                return;
            }

            var type = _prizes.FirstOrDefault(x => x.Value == Prize).Key;

            if (type is < PrizeType.Crystal or > PrizeType.GreenPendant)
            {
                throw new Exception(string.Format(
                    CultureInfo.InvariantCulture,
                    "Unexpected prize {0} to be cycled.", type.ToString()));
            }

            if (reverse)
            {
                Prize = type == PrizeType.Crystal ? null : _prizes[type - 1];
                return;
            }
            
            Prize = type == PrizeType.GreenPendant ? null : _prizes[type + 1];
        }

        public IUndoable CreateChangePrizeAction()
        {
            return _changePrizeFactory(this);
        }

        public void Reset()
        {
            Prize = _startingPrize;
        }

        /// <summary>
        ///     Returns a new prize placement save data instance for this prize placement.
        /// </summary>
        /// <returns>
        ///     A new prize placement save data instance.
        /// </returns>
        public PrizePlacementSaveData Save()
        {
            var prize = Prize is null ? (PrizeType?) null
                : _prizes.FirstOrDefault(x => x.Value == Prize).Key;

            return new PrizePlacementSaveData()
            {
                Prize = prize
            };
        }

        /// <summary>
        ///     Loads prize placement save data.
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
