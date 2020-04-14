using Avalonia.Media;
using OpenTracker.Actions;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class ItemControlVM : ViewModelBase, IItemControlVM
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettingsVM _appSettings;
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

        public ItemControlVM(UndoRedoManager undoRedoManager, AppSettingsVM appSettings,
            Item[] items)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings;
            _items = items;

            if (_items != null)
            {
                _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/" + _items[0].Type.ToString().ToLower();

                UpdateImage();

                if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
                {
                    UpdateText();
                    UpdateTextColor();
                }

                foreach (Item item in _items)
                    item.PropertyChanged += OnItemChanged;
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateImage();

            if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
            {
                UpdateText();
                UpdateTextColor();
            }
        }

        private void UpdateImage()
        {
            if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
                ImageSource = _imageSourceBase + ".png";
            else
            {
                int imageNumber = _items[0].Current;

                if (_items.Length == 2)
                    imageNumber += _items[1].Current * (_items[0].Maximum + 1);

                ImageSource = _imageSourceBase + imageNumber.ToString() + ".png";
            }
        }

        private void UpdateText()
        {
            if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
                ImageNumber = (7 - _items[0].Current).ToString();
        }

        private void UpdateTextColor()
        {
            if (_items[0].Current == 0)
                TextColor = _appSettings.EmphasisFontColor;
            else
                TextColor = Brushes.White;
        }

        public void ChangeItem(bool rightClick = false)
        {
            if (_items != null)
            {
                if (_items.Length == 2)
                {
                    if (rightClick)
                        _undoRedoManager.Execute(new CycleItem(_items[1]));
                    else
                        _undoRedoManager.Execute(new CycleItem(_items[0]));
                }
                else
                {
                    if (rightClick)
                    {
                        if (_items[0].Current > 0)
                            _undoRedoManager.Execute(new RemoveItem(_items[0]));
                    }
                    else
                    {
                        if (_items[0].Current < _items[0].Maximum)
                            _undoRedoManager.Execute(new AddItem(_items[0]));
                    }
                }
            }
        }
    }
}