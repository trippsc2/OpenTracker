using OpenTracker.Interfaces;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Items;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This is the ViewModel of the large Items panel control representing crystal requirements.
    /// </summary>
    public class CrystalRequirementLargeItemVM : LargeItemVMBase, IClickHandler
    {
        private readonly ICrystalRequirementItem _item;

        public string ImageSource { get; }

        public string ImageCount
        {
            get
            {
                if (_item.Known)
                {
                    return (7 - _item.Current).ToString(CultureInfo.InvariantCulture);
                }

                return "?";
            }
        }

        public string TextColor
        {
            get
            {
                if (_item.Known)
                {
                    return _item.Current == 0 ?
                        AppSettings.Instance.Colors.EmphasisFontColor : "#ffffffff";
                }

                return AppSettings.Instance.Colors.AccessibilityColors[AccessibilityLevel.SequenceBreak];
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageSource">
        /// A string representing the image source.
        /// </param>
        /// <param name="item">
        /// An item that is to be represented by this control.
        /// </param>
        public CrystalRequirementLargeItemVM(string imageSource, ICrystalRequirementItem item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            ImageSource = imageSource ?? throw new ArgumentNullException(nameof(imageSource));

            _item.PropertyChanged += OnItemChanged;
            AppSettings.Instance.Colors.PropertyChanged += OnColorsChanged;
            AppSettings.Instance.Colors.AccessibilityColors.PropertyChanged += OnColorsChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ICrystalRequirementItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ICrystalRequirementItem.Current) ||
                e.PropertyName == nameof(ICrystalRequirementItem.Known))
            {
                this.RaisePropertyChanged(nameof(ImageCount));
                UpdateTextColor();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ColorSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnColorsChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTextColor();
        }

        /// <summary>
        /// Raises the PropertyChanged event for the TextColor property.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(TextColor));
        }

        /// <summary>
        /// Handles left clicks and decreases the crystal requirement by one.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new AddCrystalRequirement(_item));
        }

        /// <summary>
        /// Handles right clicks and increases the crystal requirement by one.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new RemoveCrystalRequirement(_item));
        }
    }
}
