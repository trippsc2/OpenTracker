using Avalonia.Media;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class DungeonItemControlVM : ViewModelBase, IItemControlVM
    {
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

        public DungeonItemControlVM(AppSettingsVM appSettings, Mode mode, Item item)
        {
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

                SetImage();
                
                item.PropertyChanged += OnItemChanged;
                mode.PropertyChanged += OnModeChanged;
            }
        }

        private void SetImage()
        {
            if (_item == null)
                return;

            if (_smallKey && _item.Current > 0)
                TextVisible = true;
            else
                TextVisible = false;

            ImageNumber = _item.Current.ToString();

            ImageSource = _imageSourceBase + ImageNumber + ".png";

            if (_item.Current == _item.Maximum)
            {
                TextColor = _appSettings.EmphasisFontColor;
                ImageNumber += "*";
            }
            else
                TextColor = Brushes.White;

        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            SetImage();
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            SetImage();
        }

        public void ChangeItem(bool rightClick = false)
        {
            if (_item != null)
            {
                if (rightClick)
                {
                    if (_item.Current > 0)
                        _item.Change(-1);
                }
                else
                    _item.Change(1);
            }
        }
    }
}
