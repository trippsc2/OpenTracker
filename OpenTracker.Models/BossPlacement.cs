using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class for the boss placement.
    /// </summary>
    public class BossPlacement : INotifyPropertyChanged
    {
        private readonly Game _game;
        private readonly BossType _defaultBoss;

        public event PropertyChangedEventHandler PropertyChanged;

        private BossType? _boss;
        public BossType? Boss
        {
            get => _boss;
            set
            {
                if (_boss != value)
                {
                    _boss = value;
                    OnPropertyChanged(nameof(Boss));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        /// <param name="iD">
        /// The boss placement ID.
        /// </param>
        public BossPlacement(Game game, BossPlacementID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));

            switch (iD)
            {
                case BossPlacementID.ATBoss:
                case BossPlacementID.GTFinalBoss:
                    {
                        _defaultBoss = BossType.Aga;
                        Boss = BossType.Aga;
                    }
                    break;
                case BossPlacementID.EPBoss:
                    {
                        _defaultBoss = BossType.Armos;
                    }
                    break;
                case BossPlacementID.DPBoss:
                    {
                        _defaultBoss = BossType.Lanmolas;
                    }
                    break;
                case BossPlacementID.ToHBoss:
                    {
                        _defaultBoss = BossType.Moldorm;
                    }
                    break;
                case BossPlacementID.PoDBoss:
                    {
                        _defaultBoss = BossType.HelmasaurKing;
                    }
                    break;
                case BossPlacementID.SPBoss:
                    {
                        _defaultBoss = BossType.Arrghus;
                    }
                    break;
                case BossPlacementID.SWBoss:
                    {
                        _defaultBoss = BossType.Mothula;
                    }
                    break;
                case BossPlacementID.TTBoss:
                    {
                        _defaultBoss = BossType.Blind;
                    }
                    break;
                case BossPlacementID.IPBoss:
                    {
                        _defaultBoss = BossType.Kholdstare;
                    }
                    break;
                case BossPlacementID.MMBoss:
                    {
                        _defaultBoss = BossType.Vitreous;
                    }
                    break;
                case BossPlacementID.TRBoss:
                    {
                        _defaultBoss = BossType.Trinexx;
                    }
                    break;
                case BossPlacementID.GTBoss1:
                    {
                        _defaultBoss = BossType.Armos;
                    }
                    break;
                case BossPlacementID.GTBoss2:
                    {
                        _defaultBoss = BossType.Lanmolas;
                    }
                    break;
                case BossPlacementID.GTBoss3:
                    {
                        _defaultBoss = BossType.Moldorm;
                    }
                    break;
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Returns the current boss type.
        /// </summary>
        /// <returns>
        /// The boss type.
        /// </returns>
        internal BossType? GetCurrentBoss()
        {
            if (_game.Mode.BossShuffle)
            {
                return Boss;
            }

            return _defaultBoss;
        }

        /// <summary>
        /// Resets the boss placement to their starting values.
        /// </summary>
        public void Reset()
        {
            if (_defaultBoss != BossType.Aga)
            {
                Boss = null;
            }
        }
    }
}
