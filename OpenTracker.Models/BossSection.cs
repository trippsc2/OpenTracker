using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class BossSection : ISection
    {
        public string Name { get => "Boss"; }

        private bool _available;
        public bool Available
        {
            get => _available;
            set
            {
                if (_available != value)
                {
                    _available = value;
                    OnPropertyChanged(nameof(Available));
                }
            }
        }

        private Boss _boss;
        public Boss Boss
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

        private Item _prize;
        public Item Prize
        {
            get => _prize;
            set
            {
                if (_prize != value)
                {
                    if (_prize != null && !Available)
                        _prize.Current--;
                    _prize = value;
                    if (_prize != null && !Available)
                        _prize.Current++;
                    OnPropertyChanged(nameof(Prize));
                }
            }
        }

        public Func<Mode, ItemDictionary, Accessibility> GetAccessibility { get; }

        public event EventHandler ItemRequirementChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public BossSection(Game game, LocationID iD)
        {
            Available = true;
        }

        private void OnItemRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ItemRequirementChanged != null)
                ItemRequirementChanged.Invoke(this, new EventArgs());
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Available) && Prize != null)
            {
                if (Available)
                    Prize.Current--;
                else
                    Prize.Current++;
            }
        }

        public void Clear()
        {
            Available = false;
        }

        public bool IsAvailable()
        {
            return Available;
        }
    }
}
