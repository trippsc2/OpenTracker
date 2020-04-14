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

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            private set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        private string _imageNumber;
        public string ImageNumber
        {
            get => _imageNumber;
            private set => this.RaiseAndSetIfChanged(ref _imageNumber, value);
        }

        private IBrush _textColor;
        public IBrush TextColor
        {
            get => _textColor;
            private set => this.RaiseAndSetIfChanged(ref _textColor, value);
        }

        private bool _textVisible;
        public bool TextVisible
        {
            get => _textVisible;
            private set => this.RaiseAndSetIfChanged(ref _textVisible, value);
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

                UpdateImage();

                if (_smallKey)
                {
                    UpdateText();
                    UpdateTextColor();
                }
                
                item.PropertyChanged += OnItemChanged;
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_item != null)
            {
                UpdateImage();

                if (_smallKey)
                {
                    UpdateText();
                    UpdateTextColor();
                }
            }
        }

        private void UpdateImage()
        {
            ImageSource = _imageSourceBase + (_item.Current > 0 ? "1" : "0") + ".png";
        }

        private void UpdateText()
        {
            if (_item.Current > 0)
                TextVisible = true;
            else
                TextVisible = false;

            ImageNumber = _item.Current.ToString();

            if (_item.Current == _item.Maximum)
                ImageNumber += "*";
        }

        private void UpdateTextColor()
        {
            if (_item.Current == _item.Maximum)
                TextColor = _appSettings.EmphasisFontColor;
            else
                TextColor = Brushes.White;
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
