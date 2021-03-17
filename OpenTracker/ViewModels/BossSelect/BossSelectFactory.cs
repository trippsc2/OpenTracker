using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;

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

            // TODO - Convert to foreach in .NET 5
            for (var i = 0; i < Enum.GetValues(typeof(BossType)).Length; i++)
            {
                var boss = (BossType)i;
                
                if (boss != BossType.Aga && boss != BossType.Test)
                {
                    buttons.Add(_buttonFactory(bossPlacement, boss));
                }
            }

            return buttons;
        }
    }
}
