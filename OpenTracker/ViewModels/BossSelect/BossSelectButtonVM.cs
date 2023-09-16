using OpenTracker.Autofac;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.BossSelect;

/// <summary>
/// This class contains the boss select button control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class BossSelectButtonVM : ViewModel, IBossSelectButtonVM
{
    public BossType? Boss { get; }
    public string ImageSource { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="boss">
    /// The boss to be represented by this button.
    /// </param>
    /// <param name="bossPlacement">
    /// The boss placement to be manipulated.
    /// </param>
    public BossSelectButtonVM(IBossPlacement bossPlacement, BossType? boss)
    {
        Boss = boss;
        ImageSource = Boss.HasValue ? "avares://OpenTracker/Assets/Images/Bosses/" +
                                      $"{Boss.ToString()!.ToLowerInvariant()}1.png" : "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png";
    }
}