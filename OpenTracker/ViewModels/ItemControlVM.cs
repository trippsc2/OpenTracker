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

        public ItemControlVM(UndoRedoManager undoRedoManager, Item[] items)
        {
            _undoRedoManager = undoRedoManager;
            _items = items;

            if (_items != null)
            {
                _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/" + _items[0].Type.ToString().ToLower();

                SetImage();

                foreach (Item item in _items)
                    item.PropertyChanged += OnItemChanged;
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

            if (_items[0].Type != ItemType.TowerCrystals && _items[0].Type != ItemType.GanonCrystals)
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

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
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