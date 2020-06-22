using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Dictionaries
{
    /// <summary>
    /// This is the dictionary container for bosses
    /// </summary>
    public class BossDictionary : Dictionary<BossType, Boss>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; 

        private AccessibilityLevel _unknownBossAccessibility;
        public AccessibilityLevel UnknownBossAccessibility
        {
            get => _unknownBossAccessibility;
            private set
            {
                if (_unknownBossAccessibility != value)
                {
                    _unknownBossAccessibility = value;
                    OnPropertyChanged(nameof(UnknownBossAccessibility));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data parent class.
        /// </param>
        public BossDictionary(Game game) : base()
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            foreach (BossType type in Enum.GetValues(typeof(BossType)))
            {
                Add(type, new Boss(game, type));
                this[type].PropertyChanged += OnRequirementChanged;
            }

            UpdateUnknownBossAccessibility();
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
        /// Subscribes to the PropertyChanged event on the Boss classes contained.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateUnknownBossAccessibility();
        }

        /// <summary>
        /// Updates the accessibility provided to boss placements with 
        /// no boss specified in boss shuffle mode.
        /// </summary>
        private void UpdateUnknownBossAccessibility()
        {
            bool BossInaccessibility = false;

            foreach (Boss boss in Values)
            {
                if (boss.Type != BossType.Aga)
                {
                    if (boss.Accessibility < AccessibilityLevel.SequenceBreak)
                    {
                        BossInaccessibility = true;
                        break;
                    }
                }
            }

            if (BossInaccessibility)
            {
                UnknownBossAccessibility = AccessibilityLevel.SequenceBreak;
            }
            else
            {
                UnknownBossAccessibility = AccessibilityLevel.Normal;
            }
        }
    }
}
