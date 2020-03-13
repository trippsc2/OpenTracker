using Avalonia.Media;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class ItemControlVM : ViewModelBase, IItemControlVM
    {
        private readonly string _imageSourceBase;
        private readonly Item[] _items;

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

        public ItemControlVM(Item[] items)
        {
            _items = items;

            if (_items != null)
            {
                _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/" + _items[0].ItemType.ToString().ToLower();

                SetImage();

                foreach (Item item in _items)
                    item.PropertyChanged += ComponentChanged;
            }
        }

        private void SetImage()
        {
            if (_items == null)
                return;

            int imageNumber = _items[0].Current;
            string imageString = "";

            if (_items.Length == 2)
                imageNumber += _items[1].Current * (_items[0].Maximum + 1);

            if (_items[0].ItemType != ItemType.TowerCrystals && _items[0].ItemType != ItemType.GanonCrystals)
            {
                imageString = imageNumber.ToString();
                ImageNumber = "";
            }
            else
            {
                ImageNumber = (7 - _items[0].Current).ToString();

                if (_items[0].Current == 0)
                    TextColor = Brushes.Lime;
                else
                    TextColor = Brushes.White;
            }

            ImageSource = _imageSourceBase + imageString + ".png";
        }

        private void ComponentChanged(object sender, PropertyChangedEventArgs e)
        {
            SetImage();
        }

        public void ChangeItem(bool rightClick = false)
        {
            if (_items != null)
            {
                if (_items.Length == 2)
                {
                    if (rightClick)
                    {
                        if (_items[1].Current == _items[1].Maximum)
                            _items[1].Current = 0;
                        else
                            _items[1].Current++;
                    }
                    else
                    {
                        if (_items[0].Current == _items[0].Maximum)
                            _items[0].Current = 0;
                        else
                            _items[0].Current++;
                    }
                }
                else
                {
                    if (rightClick)
                        _items[0].Current = Math.Max(_items[0].Current - 1, 0);
                    else
                        _items[0].Current = Math.Min(_items[0].Current + 1, _items[0].Maximum);
                }
            }
        }
    }
}