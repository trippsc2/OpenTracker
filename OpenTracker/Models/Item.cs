using OpenTracker.Enums;
using System;
using System.ComponentModel;

namespace OpenTracker.Models
{
    public class Item : INotifyPropertyChanged
    {
        private readonly bool _cycle;

        public event PropertyChangedEventHandler PropertyChanged;

        public ItemType ItemType { get; }
        public int Maximum { get; }

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
            ItemType = itemType;

            switch (ItemType)
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
                    _cycle = true;
                    Maximum = 1;
                    break;
                case ItemType.BigBomb:
                case ItemType.MagicBat:
                case ItemType.FluteActivated:
                    _cycle = true;
                    Maximum = 1;
                    break;
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
                    Maximum = 1;
                    break;
                case ItemType.GoMode:
                case ItemType.Aga:
                    Maximum = 1;
                    break;
                case ItemType.Mushroom:
                case ItemType.Gloves:
                case ItemType.Mail:
                    Maximum = 2;
                    break;
                case ItemType.TowerCrystals:
                case ItemType.GanonCrystals:
                    Maximum = 7;
                    break;
                case ItemType.BombosDungeons:
                case ItemType.EtherDungeons:
                case ItemType.QuakeDungeons:
                    _cycle = true;
                    Maximum = 3;
                    break;
                case ItemType.Bottle:
                    Maximum = 4;
                    break;
                case ItemType.Sword:
                    Maximum = 5;
                    Current = 1;
                    break;
                case ItemType.Shield:
                    Maximum = 3;
                    break;
                case ItemType.GreenPendant:
                    Maximum = 1;
                    Current = 1;
                    break;
                case ItemType.Pendant:
                    Maximum = 2;
                    Current = 2;
                    break;
                case ItemType.Crystal:
                    Maximum = 5;
                    break;
                case ItemType.RedCrystal:
                    Maximum = 2;
                    break;
                case ItemType.HCSmallKey:
                    break;
                case ItemType.DPSmallKey:
                    break;
                case ItemType.ToHSmallKey:
                    break;
                case ItemType.ATSmallKey:
                    break;
                case ItemType.PoDSmallKey:
                    break;
                case ItemType.SPSmallKey:
                    break;
                case ItemType.SWSmallKey:
                    break;
                case ItemType.TTSmallKey:
                    break;
                case ItemType.IPSmallKey:
                    break;
                case ItemType.MMSmallKey:
                    break;
                case ItemType.TRSmallKey:
                    break;
                case ItemType.GTSmallKey:
                    break;
                case ItemType.EPBigKey:
                    break;
                case ItemType.DPBigKey:
                    break;
                case ItemType.ToHBigKey:
                    break;
                case ItemType.PoDBigKey:
                    break;
                case ItemType.SPBigKey:
                    break;
                case ItemType.SWBigKey:
                    break;
                case ItemType.TTBigKey:
                    break;
                case ItemType.IPBigKey:
                    break;
                case ItemType.MMBigKey:
                    break;
                case ItemType.TRBigKey:
                    break;
                case ItemType.GTBigKey:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemType));
            }
        }

        public void Change(int delta)
        {
            if (_cycle && delta == 1 && Current + delta > Maximum)
                Current = 0;
            else
                Current = Math.Min(Math.Max(Current + delta, 0), Maximum);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
