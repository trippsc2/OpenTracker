using OpenTracker.Models.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Item : INotifyPropertyChanged
    {
        private readonly int _starting;

        public ItemType Type { get; }
        public int Maximum { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _current;
        public int Current
        {
            get => _current;
            private set
            {
                if (_current != value)
                {
                    _current = value;
                    OnPropertyChanged(nameof(Current));
                }
            }
        }

        public Item(ItemType itemType)
        {
            Type = itemType;

            switch (Type)
            {
                case ItemType.Bow:
                case ItemType.SilverArrows:
                case ItemType.Boomerang:
                case ItemType.RedBoomerang:
                case ItemType.Bomb:
                case ItemType.Powder:
                case ItemType.Bombos:
                case ItemType.Ether:
                case ItemType.Quake:
                case ItemType.Flute:
                case ItemType.BigBomb:
                case ItemType.MagicBat:
                case ItemType.FluteActivated:
                case ItemType.Hookshot:
                case ItemType.FireRod:
                case ItemType.IceRod:
                case ItemType.Shovel:
                case ItemType.Lamp:
                case ItemType.Hammer:
                case ItemType.Net:
                case ItemType.Book:
                case ItemType.MoonPearl:
                case ItemType.CaneOfSomaria:
                case ItemType.CaneOfByrna:
                case ItemType.Cape:
                case ItemType.Mirror:
                case ItemType.Boots:
                case ItemType.Flippers:
                case ItemType.HalfMagic:
                case ItemType.HCSmallKey:
                case ItemType.GoMode:
                case ItemType.Aga:
                case ItemType.GreenPendant:
                case ItemType.DPSmallKey:
                case ItemType.ToHSmallKey:
                case ItemType.SPSmallKey:
                case ItemType.TTSmallKey:
                case ItemType.EPBigKey:
                case ItemType.DPBigKey:
                case ItemType.ToHBigKey:
                case ItemType.PoDBigKey:
                case ItemType.SPBigKey:
                case ItemType.SWBigKey:
                case ItemType.TTBigKey:
                case ItemType.IPBigKey:
                case ItemType.MMBigKey:
                case ItemType.TRBigKey:
                case ItemType.GTBigKey:
                    Maximum = 1;
                    break;
                case ItemType.Mushroom:
                case ItemType.Gloves:
                case ItemType.Mail:
                case ItemType.Pendant:
                case ItemType.RedCrystal:
                case ItemType.ATSmallKey:
                case ItemType.IPSmallKey:
                    Maximum = 2;
                    break;
                case ItemType.TowerCrystals:
                case ItemType.GanonCrystals:
                    Maximum = 7;
                    break;
                case ItemType.BombosDungeons:
                case ItemType.EtherDungeons:
                case ItemType.QuakeDungeons:
                case ItemType.Shield:
                case ItemType.SWSmallKey:
                case ItemType.MMSmallKey:
                    Maximum = 3;
                    break;
                case ItemType.Bottle:
                case ItemType.TRSmallKey:
                case ItemType.GTSmallKey:
                    Maximum = 4;
                    break;
                case ItemType.Sword:
                    Maximum = 5;
                    _starting = 1;
                    Current = 1;
                    break;
                case ItemType.Crystal:
                    Maximum = 5;
                    break;
                case ItemType.PoDSmallKey:
                    Maximum = 6;
                    break;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Change(int delta, bool ignoreMaximum = false)
        {
            if (ignoreMaximum)
                Current += delta;
            else
                Current = Math.Min(Maximum, Current + delta);
        }

        public void SetCurrent(int current = 0)
        {
            Current = current;
        }

        public void Reset()
        {
            Current = _starting;
        }
    }
}
