using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class StatusBarVM : ViewModelBase
    {
        private AutoTrackerDialogVM _autoTracker;

        public string StatusTextColor =>
            _autoTracker.StatusTextColor;
        public string StatusText =>
            _autoTracker.StatusText;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autoTracker">
        /// The ViewModel of the autotracker dialog window.
        /// </param>
        public StatusBarVM(AutoTrackerDialogVM autoTracker)
        {
            _autoTracker = autoTracker ?? throw new ArgumentNullException(nameof(autoTracker));
            _autoTracker.PropertyChanged += OnAutoTrackerChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AutoTrackerDialogVM class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackerChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AutoTrackerDialogVM.StatusTextColor))
            {
                this.RaisePropertyChanged(nameof(StatusTextColor));
            }

            if (e.PropertyName == nameof(AutoTrackerDialogVM.StatusText))
            {
                this.RaisePropertyChanged(nameof(StatusText));
            }
        }
    }
}
