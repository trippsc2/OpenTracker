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

        public string ImageSource
        {
            get
            {
                if (_items == null || _items[0] == null)
                    return null;

                if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
                    return _imageSourceBase + ".png";
                else
                {
                    int imageNumber = _items[0].Current;

                    if (_items.Length == 2)
                        imageNumber += _items[1].Current * (_items[0].Maximum + 1);

                    return _imageSourceBase + imageNumber.ToString() + ".png";
                }
            }
        }

        public string ImageNumber
        {
            get
            {
                if (_items == null || _items[0] == null)
                    return null;

                if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
                    return (7 - _items[0].Current).ToString();

                return null;
            }
        }
        
        public IBrush TextColor
        {
            get
            {
                if (_items == null || _items[0] == null)
                    return Brushes.White;

                if (_items[0].Current == 0)
                    return _appSettings.EmphasisFontColor;
                else
                    return Brushes.White;
            }
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

                foreach (Item item in _items)
                    item.PropertyChanged += OnItemChanged;
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(ImageSource));

            if (_items[0].Type == ItemType.TowerCrystals || _items[0].Type == ItemType.GanonCrystals)
            {
                this.RaisePropertyChanged(nameof(ImageNumber));
                this.RaisePropertyChanged(nameof(TextColor));
            }
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