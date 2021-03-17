using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.AutoTracking
{
    /// <summary>
    /// This class contains the auto-tracker status text control ViewModel data.
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autoTracker">
        /// The auto-tracker data.
        /// </param>
        public AutoTrackerStatusVM(IAutoTracker autoTracker)
        {
            _autoTracker = autoTracker;

            _autoTracker.PropertyChanged += OnAutoTrackerChanged;

            switch (_autoTracker.Status)
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
                    sb.Append(_autoTracker.RaceIllegalTracking ? "RACE ILLEGAL)" : "RACE LEGAL)");
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
        private async void OnAutoTrackerChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAutoTracker.RaceIllegalTracking) ||
                e.PropertyName == nameof(IAutoTracker.Status))
            {
                await UpdateStatusText();
            }
        }

        /// <summary>
        /// Updates the status text and text color based on the status of the SNES connector.
        /// </summary>
        private async Task UpdateStatusText()
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                switch (_autoTracker.Status)
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
                            sb.Append(_autoTracker.RaceIllegalTracking ? "RACE ILLEGAL)" : "RACE LEGAL)");
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
