﻿using Avalonia.Media;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This is the ViewModel of the section control.
    /// </summary>
    public class SectionVM : ViewModelBase, ISectionVM
    {
        private readonly IColorSettings _colorSettings;
        private readonly ISection _section;

        public Color FontColor =>
            Color.Parse(_colorSettings.AccessibilityColors[_section.Accessibility]);
        public bool Visible =>
            _section.Requirement.Met;
        public string Name =>
            _section.Name;
        public bool NormalAccessibility =>
            _section.Accessibility == AccessibilityLevel.Normal;

        public List<ISectionIconVMBase> Icons { get; }

        public delegate ISectionVM Factory(ISection section, List<ISectionIconVMBase> icons);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The section to be represented.
        /// </param>
        /// <param name="icons">
        /// The observable collection of section icon control ViewModel instances.
        /// </param>
        public SectionVM(
            IColorSettings colorSettings, ISection section, List<ISectionIconVMBase> icons)
        {
            _colorSettings = colorSettings;
            _section = section;
            Icons = icons;

            _colorSettings.AccessibilityColors.PropertyChanged += OnColorChanged;
            _section.PropertyChanged += OnSectionChanged;
            _section.Requirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ObservableCollection for the
        /// accessibility colors.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTextColor();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Accessibility))
            {
                UpdateTextColor();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                this.RaisePropertyChanged(nameof(Visible));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the FontColor and NormalAccessibility properties.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(FontColor));
            this.RaisePropertyChanged(nameof(NormalAccessibility));
        }
    }
}