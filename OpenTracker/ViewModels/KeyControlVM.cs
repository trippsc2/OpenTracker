﻿using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels
{
    public class KeyControlVM : ViewModelBase, IClickHandler
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly Game _game;
        private readonly string _imageSourceBase;
        private readonly Item _item;
        private readonly bool _smallKey;

        public bool SmallKeyShuffle =>
            _game.Mode.SmallKeyShuffle;

        public bool BigKeyShuffle =>
            _game.Mode.BigKeyShuffle;

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
                    return _item.Current.ToString(CultureInfo.InvariantCulture) + (_item.Current == _item.Maximum ? "*" : "");

                return null;
            }
        }

        public bool TextVisible => _smallKey && _item.Current > 0;

        public string TextColor
        {
            get
            {
                if (_item == null)
                    return "#ffffffff";

                if (_item.Current == _item.Maximum)
                    return _appSettings.EmphasisFontColor;
                else
                    return "#ffffffff";
            }
        }

        public KeyControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Game game, Item item)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _item = item;

            _appSettings.PropertyChanged += OnAppSettingsChanged;
            _game.Mode.PropertyChanged += OnModeChanged;

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

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.EmphasisFontColor))
                this.RaisePropertyChanged(nameof(TextColor));
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.WorldState))
                this.RaisePropertyChanged(nameof(SmallKeyShuffle));

            if (e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                this.RaisePropertyChanged(nameof(SmallKeyShuffle));
                this.RaisePropertyChanged(nameof(BigKeyShuffle));
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

        private void AddItem()
        {
            _undoRedoManager.Execute(new AddItem(_item));
        }

        private void RemoveItem()
        {
            _undoRedoManager.Execute(new RemoveItem(_item));
        }

        public void OnLeftClick(bool force = false)
        {
            if (_item != null && _item.Current < _item.Maximum)
                AddItem();
        }

        public void OnRightClick(bool force = false)
        {
            if (_item != null && _item.Current > 0)
                RemoveItem();
        }
    }
}