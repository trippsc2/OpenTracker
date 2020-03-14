using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class RegionSection : ISection
    {
        public string Name { get => ""; }

        public Func<Mode, ItemDictionary, Accessibility> GetAccessibility { get; }

        public event EventHandler ItemRequirementChanged;
        public event PropertyChangedEventHandler PropertyChanged;

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

        public RegionSection()
        {
            GetAccessibility = (mode, items) => { return Accessibility.Normal; };
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
