﻿using Avalonia.Threading;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Text;

namespace OpenTracker.ViewModels.AutoTracking
{
    /// <summary>
    /// This is the class for the autotracker status text ViewModel.
    /// </summary>
    public class AutoTrackerStatusVM : ViewModelBase, IAutoTrackerStatusVM
    {
        private readonly IAutoTracker _autoTracker;

        private string _statusTextColor = "#ffffff";
        public string StatusTextColor
        {
            get => _statusTextColor;
            private set => this.RaiseAndSetIfChanged(ref _statusTextColor, value);
        }

        private string _statusText = "NOT CONNECTED";
        public string StatusText
        {
            get => _statusText;
            private set => this.RaiseAndSetIfChanged(ref _statusText, value);
        }

        public AutoTrackerStatusVM() : this(AutoTracker.Instance)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autoTracker">
        /// The autotracker.
        /// </param>
        private AutoTrackerStatusVM(IAutoTracker autoTracker)
        {
            _autoTracker = autoTracker;

            _autoTracker.PropertyChanged += OnAutoTrackerChanged;
            _autoTracker.SNESConnector.PropertyChanged += OnConnectorChanged;

            UpdateStatusText();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AutoTracker class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAutoTrackerChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTracker.RaceIllegalTracking))
            {
                UpdateStatusText();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISNESConnector interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnConnectorChanged(object? sender, PropertyChangedEventArgs e)
        {
            UpdateStatusText();
        }

        /// <summary>
        /// Updates the status text and text color based on the status of the SNES connector.
        /// </summary>
        private void UpdateStatusText()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                switch (_autoTracker.SNESConnector.Status)
                {
                    case ConnectionStatus.NotConnected:
                        {
                            StatusTextColor = "#ffffff";
                            StatusText = "NOT CONNECTED";
                        }
                        break;
                    case ConnectionStatus.SelectDevice:
                        {
                            StatusTextColor = "#ffffff";
                            StatusText = "SELECT DEVICE";
                        }
                        break;
                    case ConnectionStatus.Connecting:
                        {
                            StatusTextColor = "#ffff00";
                            StatusText = "CONNECTING";
                        }
                        break;
                    case ConnectionStatus.Attaching:
                        {
                            StatusTextColor = "#ffff00";
                            StatusText = "ATTACHING";
                        }
                        break;
                    case ConnectionStatus.Connected:
                        {
                            StatusTextColor = "#00ff00";
                            var sb = new StringBuilder();
                            sb.Append("CONNECTED (");

                            if (_autoTracker.RaceIllegalTracking)
                            {
                                sb.Append("RACE ILLEGAL)");
                            }
                            else
                            {
                                sb.Append("RACE LEGAL)");
                            }

                            StatusText = sb.ToString();
                        }
                        break;
                    case ConnectionStatus.Error:
                        {
                            StatusTextColor = "#ff3030";
                            StatusText = "ERROR";
                        }
                        break;
                }
            });
        }
    }
}