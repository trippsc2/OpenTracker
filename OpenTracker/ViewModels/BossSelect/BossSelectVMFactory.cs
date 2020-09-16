using OpenTracker.Models.BossPlacements;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.BossSelect
{
    /// <summary>
    /// This is the class for creating boss select control ViewModel classes.
    /// </summary>
    internal static class BossSelectVMFactory
    {
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
        private static ObservableCollection<BossSelectButtonVM> GetBossSelectButtonVMs(
            IBossPlacement bossPlacement)
        {
            ObservableCollection<BossSelectButtonVM> buttons =
                new ObservableCollection<BossSelectButtonVM>
                {
                    new BossSelectButtonVM(null, bossPlacement)
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
                            buttons.Add(new BossSelectButtonVM(boss, bossPlacement));
                        }
                        break;
                }
            }

            return buttons;
        }

        /// <summary>
        /// Returns a new boss select popup ViewModel instance.
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss placement to be manipulated by the boss select popup.
        /// </param>
        /// <returns>
        /// A new boss select popup ViewModel instance.
        /// </returns>
        internal static BossSelectPopupVM GetBossSelectPopupVM(IBossPlacement bossPlacement)
        {
            if (bossPlacement == null)
            {
                throw new ArgumentNullException(nameof(bossPlacement));
            }

            return new BossSelectPopupVM(bossPlacement, GetBossSelectButtonVMs(bossPlacement));
        }
    }
}
