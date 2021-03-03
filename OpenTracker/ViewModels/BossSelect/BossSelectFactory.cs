using OpenTracker.Models.BossPlacements;
using System;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.BossSelect
{
    /// <summary>
    /// This class contains creation logic for the boss select controls.
    /// </summary>
    public class BossSelectFactory : IBossSelectFactory
    {
        private readonly IBossSelectButtonVM.Factory _buttonFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="buttonFactory">
        /// An Autofac factory for creating boss select button controls.
        /// </param>
        public BossSelectFactory(IBossSelectButtonVM.Factory buttonFactory)
        {
            _buttonFactory = buttonFactory;
        }

        /// <summary>
        /// Returns a list of boss select button control ViewModel instances for the specified boss placement.
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss placement.
        /// </param>
        /// <returns>
        /// An observable collection of boss select button control ViewModel instances.
        /// </returns>
        public List<IBossSelectButtonVM> GetBossSelectButtonVMs(IBossPlacement bossPlacement)
        {
            var buttons = new List<IBossSelectButtonVM>
            {
                _buttonFactory(bossPlacement, null)
            };

            foreach (BossType boss in Enum.GetValues(typeof(BossType)))
            {
                if (boss != BossType.Aga)
                {
                    buttons.Add(_buttonFactory(bossPlacement, boss));
                }
            }

            return buttons;
        }
    }
}
