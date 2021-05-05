using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using ReactiveUI;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This class contains auto-tracking logic and data.
    /// </summary>
    public class AutoTracker : ReactiveObject, IAutoTracker
    {
        private readonly IMemoryAddressProvider _memoryAddressProvider;
        private readonly ISNESConnector _snesConnector;

        private IMemoryAddress InGameStatus { get; }

        private bool CanReadMemory => Status == ConnectionStatus.Connected;
        private bool IsInGame
        {
            get
            {
                var inGameValue = InGameStatus.Value;
                
                if (inGameValue is null)
                {
                    return false;
                }
                
                return inGameValue > 0x05 && inGameValue != 0x14 && inGameValue < 0x20;
            }
        }

        private IList<string> _devices = new List<string>();
        public IList<string> Devices
        {
            get => _devices;
            private set => this.RaiseAndSetIfChanged(ref _devices, value);
        }

        private bool _raceIllegalTracking;
        public bool RaceIllegalTracking
        {
            get => _raceIllegalTracking;
            set => this.RaiseAndSetIfChanged(ref _raceIllegalTracking, value);
        }

        public ConnectionStatus Status => _snesConnector.Status;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoryAddressProvider">
        ///     The <see cref="IMemoryAddressProvider"/>.
        /// </param>
        /// <param name="snesConnector">
        ///     The <see cref="ISNESConnector"/>.
        /// </param>
        public AutoTracker(IMemoryAddressProvider memoryAddressProvider, ISNESConnector snesConnector)
        {
            _memoryAddressProvider = memoryAddressProvider;
            _snesConnector = snesConnector;

            InGameStatus = _memoryAddressProvider.MemoryAddresses[0x7e0010];

            _snesConnector.PropertyChanged += OnSNESConnectorChanged;
        }

        public bool CanConnect()
        {
            return Status == ConnectionStatus.NotConnected;
        }

        public async Task Connect(string uriString)
        {
            _snesConnector.SetURI(uriString);
            await _snesConnector.ConnectAsync();
        }

        public bool CanGetDevices()
        {
            return Status == ConnectionStatus.SelectDevice;
        }

        public async Task GetDevices()
        {
            Devices = await GetDevicesFromConnector();
        }

        public bool CanDisconnect()
        {
            return Status != ConnectionStatus.NotConnected;
        }

        public async Task Disconnect()
        { 
            await _snesConnector.DisconnectAsync();
            _memoryAddressProvider.Reset();
        }

        public bool CanStart()
        {
            return Status == ConnectionStatus.SelectDevice;
        }

        public async Task Start(string device)
        {
            await _snesConnector.SetDeviceAsync(device);
        }

        public async Task InGameCheck()
        {
            if (!CanReadMemory)
            {
                return;
            }

            var result = await _snesConnector.ReadMemoryAsync(0x7e0010);

            if (result is null)
            {
                return;
            }

            InGameStatus.Value = result[0];
        }

        public async Task MemoryCheck()
        {
            if (!CanReadMemory || !IsInGame)
            {
                return;
            }

            foreach (MemorySegmentType type in Enum.GetValues(typeof(MemorySegmentType)))
            {
                await MemoryCheck(type);
            }
        }

        /// <summary>
        /// Subscribes to the <see cref="ISNESConnector.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender">
        ///     The <see cref="object"/> from which the event is sent.
        /// </param>
        /// <param name="e">
        ///     The <see cref="PropertyChangedEventArgs"/>.
        /// </param>
        private void OnSNESConnectorChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISNESConnector.Status))
            {
                this.RaisePropertyChanged(nameof(Status));
            }
        }

        /// <summary>
        /// Returns the <see cref="IList{T}"/> of devices provided by the SNES connector.
        /// </summary>
        /// <returns>
        ///     A <see cref="IList{T}"/> of <see cref="string"/> representing the devices.
        /// </returns>
        private async Task<IList<string>> GetDevicesFromConnector()
        {
            var devices = await _snesConnector.GetDevicesAsync();

            return devices is null ? new List<string>() : new List<string>(devices);
        }
        
        /// <summary>
        /// Updates cached values of the specified memory segment.
        /// </summary>
        /// <param name="segment">
        ///     The <see cref="MemorySegmentType"/> to be updated.
        /// </param>
        private async Task MemoryCheck(MemorySegmentType segment)
        {
            var memorySegment = _memoryAddressProvider.MemorySegments[segment];
            var startAddress = _memoryAddressProvider.GetMemorySegmentStart(segment);
            var buffer = await _snesConnector.ReadMemoryAsync(startAddress, memorySegment.Count);

            if (buffer is not null)
            {
                for (var i = 0; i < buffer.Length; i++)
                {
                    memorySegment[i].Value = buffer[i];
                }
            }
        }
    }
}
