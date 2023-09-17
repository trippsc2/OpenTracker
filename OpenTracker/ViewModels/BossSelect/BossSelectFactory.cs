using System;
using System.Collections.Generic;
using System.Reactive;
using OpenTracker.Autofac;
using OpenTracker.Models.BossPlacements;
using ReactiveUI;

namespace OpenTracker.ViewModels.BossSelect;

/// <summary>
/// This class contains creation logic for the boss select controls.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class BossSelectFactory : IBossSelectFactory
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
    /// <param name="changeBossCommand">
    ///
    /// </param>
    /// <returns>
    /// An observable collection of boss select button control ViewModel instances.
    /// </returns>
    public List<IBossSelectButtonVM> GetBossSelectButtonVMs(
        IBossPlacement bossPlacement,
        ReactiveCommand<BossType?, Unit> changeBossCommand)
    {
        var buttons = new List<IBossSelectButtonVM>
        {
            _buttonFactory(bossPlacement, null, changeBossCommand)
        };

        foreach (BossType boss in Enum.GetValues(typeof(BossType)))
        {
            if (boss != BossType.Aga && boss != BossType.Test)
            {
                buttons.Add(_buttonFactory(bossPlacement, boss, changeBossCommand));
            }
        }

        return buttons;
    }
}