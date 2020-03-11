using Avalonia.Media;
using OpenTracker.Enums;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using ReactiveUI;
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

        public void ChangeItem(bool alternate = false)
        {
            if (_items != null)
            {
                if (alternate)
                {
                    if (_items.Length == 2)
                        _items[1].Change(1);
                    else
                        _items[0].Change(-1);
                }
                else
                    _items[0].Change(1);
            }
        }
    }
}