using OpenTracker.Interfaces;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This is the ViewModel for the pinned location control.
    /// </summary>
    public class PinnedLocationVM : ViewModelBase, IModelWrapper, IClickHandler
    {
        private readonly ILocation _location;

        public static double Scale =>
            AppSettings.Instance.Layout.UIScale;
        public string Name =>
            _location.Name;
        public object Model =>
            _location;

        public List<SectionVM> Sections { get; }
        public PinnedLocationNoteAreaVM Notes { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        /// <param name="sections">
        /// The observable collection of section control ViewModels.
        /// </param>
        public PinnedLocationVM(ILocation location, List<SectionVM> sections)
        {
            _location = location ?? throw new ArgumentNullException(nameof(location));
            Sections = sections ?? throw new ArgumentNullException(nameof(sections));
            Notes = new PinnedLocationNoteAreaVM(_location);

            AppSettings.Instance.Layout.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }

        /// <summary>
        /// Handles left clicks and removes the pinned location.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            UndoRedoManager.Instance.Execute(new UnpinLocation(_location));
        }

        /// <summary>
        /// Handles right clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
        }
    }
}