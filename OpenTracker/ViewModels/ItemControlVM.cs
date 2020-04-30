using Avalonia.Media;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class ItemControlVM : ViewModelBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
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
        
        public string TextColor
        {
            get
            {
                if (_items == null || _items[0] == null)
                    return "#ffffffff";

                if (_items[0].Current == 0)
                    return _appSettings.EmphasisFontColor;
                else
                    return "#ffffffff";
            }
        }

        public ItemControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Item[] items)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings;
            _items = items;

            _appSettings.PropertyChanged += OnAppSettingsChanged;

            if (_items != null)
            {
                _imageSourceBase = "avares://OpenTracker/Assets/Images/Items/" + _items[0].Type.ToString().ToLower();

                foreach (Item item in _items)
                    item.PropertyChanged += OnItemChanged;
            }
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.EmphasisFontColor))
                this.RaisePropertyChanged(nameof(TextColor));
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

        private void AddItem(int index)
        {
            _undoRedoManager.Execute(new AddItem(_items[index]));
        }

        private void RemoveItem(int index)
        {
            _undoRedoManager.Execute(new RemoveItem(_items[index]));
        }

        private void CycleItem(int index)
        {
            _undoRedoManager.Execute(new CycleItem(_items[index]));
        }

        public void OnLeftClick()
        {
            if (_items != null)
            {
                if (_items.Length > 1)
                    CycleItem(0);
                else if (_items[0].Current < _items[0].Maximum)
                    AddItem(0);
            }
        }

        public void OnRightClick()
        {
            if (_items != null)
            {
                if (_items.Length > 1)
                    CycleItem(1);
                else if (_items[0].Current > 0)
                    RemoveItem(0);
            }
        }
    }
}