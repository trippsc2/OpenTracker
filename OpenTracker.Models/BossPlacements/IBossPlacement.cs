using OpenTracker.Models.Enums;
using System.ComponentModel;

namespace OpenTracker.Models.BossPlacements
{
    public interface IBossPlacement : INotifyPropertyChanged
    {
        BossType? Boss { get; set; }

        BossType? GetCurrentBoss();
        void Reset();
    }
}