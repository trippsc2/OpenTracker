using Avalonia.Media;
using OpenTracker.Actions;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class DungeonItemControlVM : ViewModelBase, IItemControlVM
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettingsVM _appSettings;
        private readonly string _imageSourceBase;
        private readonly Item _item;
        private readonly bool _smallKey;

        public string ImageSource
        {
            get
            {
                if (_item == null)
                    return null;

                return _imageSourceBase + (_item.Current > 0 ? "1" : "0") + ".png";
            }
        }

        public string ImageNumber
        {
            get
            {
                if (_smallKey)
                    return _item.Current.ToString() + (_item.Current == _item.Maximum ? "*" : "");

                return null;
            }
        }

        public bool TextVisible => _smallKey && _item.Current > 0;

        public IBrush TextColor
        {
            get
            {
                if (_item == null)
                    return Brushes.White;

                if (_item.Current == _item.Maximum)
                    return _appSettings.EmphasisFontColor;
                else
                    return Brushes.White;
            }
        }

        public DungeonItemControlVM(UndoRedoManager undoRedoManager, AppSettingsVM appSettings,
            Item item)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings;
            _item = item;

            if (_item != null)
            {
                switch (_item.Type)
                {
                    case ItemType.HCSmallKey:
                    case ItemType.DPSmallKey:
                    case ItemType.ToHSmallKey:
                    case ItemType.ATSmallKey:
                    case ItemType.PoDSmallKey:
                    case ItemType.SPSmallKey:
                    case ItemType.SWSmallKey:
                    case ItemType.TTSmallKey:
                    case ItemType.IPSmallKey:
                    case ItemType.MMSmallKey:
                    case ItemType.TRSmallKey:
                    case ItemType.GTSmallKey:
                        _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/smallkey";
                        _smallKey = true;
                        break;
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
                        _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/bigkey";
                        break;
                }

                item.PropertyChanged += OnItemChanged;
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(ImageSource));
            
            if (_smallKey)
            {
                UpdateText();
                this.RaisePropertyChanged(nameof(TextColor));
            }
        }

        private void UpdateText()
        {
            this.RaisePropertyChanged(nameof(TextVisible));
            this.RaisePropertyChanged(nameof(ImageNumber));
        }

        public void ChangeItem(bool rightClick = false)
        {
            if (_item != null)
            {
                if (rightClick)
                {
                    if (_item.Current > 0)
                        _undoRedoManager.Execute(new RemoveItem(_item));
                }
                else
                {
                    if (_item.Current < _item.Maximum)
                        _undoRedoManager.Execute(new AddItem(_item));
                }
            }
        }
    }
}
