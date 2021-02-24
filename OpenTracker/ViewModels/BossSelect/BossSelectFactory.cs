using OpenTracker.Models.BossPlacements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.BossSelect
{
    /// <summary>
    /// This is the class for creating boss select control ViewModel classes.
    /// </summary>
    public class BossSelectFactory : IBossSelectFactory
    {
        private readonly IBossSelectButtonVM.Factory _buttonFactory;

        public BossSelectFactory(IBossSelectButtonVM.Factory buttonFactory)
        {
            _buttonFactory = buttonFactory;
        }

        /// <summary>
        /// Returns an observable collection of boss select button control ViewModel instances
        /// for the specified boss placement.
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss placement.
        /// </param>
        /// <returns>
        /// An observable collection of boss select button control ViewModel instances.
        /// </returns>
        public List<IBossSelectButtonVM> GetBossSelectButtonVMs(
            IBossPlacement bossPlacement)
        {
            var buttons = new List<IBossSelectButtonVM>
                {
                    _buttonFactory(bossPlacement, null)
                };

            foreach (BossType boss in Enum.GetValues(typeof(BossType)))
            {
                switch (boss)
                {
                    case BossType.Armos:
                    case BossType.Lanmolas:
                    case BossType.Moldorm:
                    case BossType.HelmasaurKing:
                    case BossType.Arrghus:
                    case BossType.Mothula:
                    case BossType.Blind:
                    case BossType.Kholdstare:
                    case BossType.Vitreous:
                    case BossType.Trinexx:
                        {
                            buttons.Add(_buttonFactory(bossPlacement, boss));
                        }
                        break;
                }
            }

            return buttons;
        }
    }
}
