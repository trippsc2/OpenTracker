using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.BossSelect;

/// <summary>
/// This class contains creation logic for the boss select controls.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class BossSelectFactory : IBossSelectFactory
{
    private readonly BossSelectButtonVM.Factory _buttonFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="buttonFactory">
    /// An Autofac factory for creating boss select button controls.
    /// </param>
    public BossSelectFactory(BossSelectButtonVM.Factory buttonFactory)
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
    public List<BossSelectButtonVM> GetBossSelectButtonVMs(IBossPlacement bossPlacement)
    {
        var buttons = new List<BossSelectButtonVM>
        {
            _buttonFactory(bossPlacement, null)
        };

        var bossButtons = Enum
            .GetValues<BossType>()
            .Where(boss => boss != BossType.Aga && boss != BossType.Test)
            .Select(boss => _buttonFactory(bossPlacement, boss));
        
        buttons.AddRange(bossButtons);

        return buttons;
    }
}