using OpenTracker.Models.AutoTracking.SNESConnectors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using WebSocketSharp;

namespace OpenTracker.Models.AutoTracking
{
    /// <summary>
    /// This is the class containing autotracking data and methods
    /// </summary>
    public class AutoTracker : IAutoTracker
    {
        private readonly IMemoryAddress.Factory _addressFactory;

        private byte? _inGameStatus;
        private readonly Dictionary<MemorySegmentType, List<IMemoryAddress>> _memorySegments =
            new Dictionary<MemorySegmentType, List<IMemoryAddress>>();

        private bool CanReadMemory =>
            Status == ConnectionStatus.Connected;
        private bool IsInGame =>
            !(_inGameStatus is null) && _inGameStatus > 0x05 && _inGameStatus != 0x14 &&
            _inGameStatus < 0x20;

        public Action<LogLevel, string>? LogHandler { get; set; }

        public Dictionary<ulong, IMemoryAddress> MemoryAddresses { get; } =
            new Dictionary<ulong, IMemoryAddress>();

        public event PropertyChangedEventHandler? PropertyChanged;

        private ISNESConnector? _snesConnector;
        private ISNESConnector SNESConnector
        {
            get => _snesConnector!;
            set
            {
                if (_snesConnector == value)
                {
                    return;
                }
                
                if (!(_snesConnector is null))
                {
                    _snesConnector.StatusChanged -= OnStatusChanged;
                }
                
                _snesConnector = value;
                _snesConnector.StatusChanged += OnStatusChanged;
                Status = ConnectionStatus.NotConnected;
            }
        }

        private List<string> _devices = new List<string>();
        public List<string> Devices
        {
            get => _devices;
            private set
            {
                if (_devices != value)
                {
                    _devices = value;
                    OnPropertyChanged(nameof(Devices));
                }
            }
        }

        private bool _raceIllegalTracking;
        public bool RaceIllegalTracking
        {
            get => _raceIllegalTracking;
            set
            {
                _raceIllegalTracking = value;
                OnPropertyChanged(nameof(RaceIllegalTracking));
            }
        }

        private ConnectionStatus _status;
        public ConnectionStatus Status
        {
            get => _status;
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="snesConnector">
        /// The SNES connector factory.
        /// </param>
        /// <param name="addressFactory">
        /// An Autofac factory for creating memory addresses.
        /// </param>
        public AutoTracker(
            ISNESConnector snesConnector, IMemoryAddress.Factory addressFactory)
        {
            _addressFactory = addressFactory;
            
            SNESConnector = snesConnector;

            foreach (MemorySegmentType type in Enum.GetValues(typeof(MemorySegmentType)))
            {
                _memorySegments.Add(type, new List<IMemoryAddress>());
            }

            for (ulong i = 0; i < 128; i++)
            {
                CreateMemoryAddress(MemorySegmentType.FirstRoom, i);
                CreateMemoryAddress(MemorySegmentType.SecondRoom, i);
                CreateMemoryAddress(MemorySegmentType.ThirdRoom, i);
                CreateMemoryAddress(MemorySegmentType.FourthRoom, i);
                CreateMemoryAddress(MemorySegmentType.FirstItem, i);
                CreateMemoryAddress(MemorySegmentType.FirstOverworldEvent, i);

                if (i < 80)
                {
                    CreateMemoryAddress(MemorySegmentType.FifthRoom, i);
                }

                if (i < 48)
                {
                    CreateMemoryAddress(MemorySegmentType.Dungeon, i);
                }

                if (i < 16)
                {
                    CreateMemoryAddress(MemorySegmentType.SecondItem, i);
                }
                
                if (i < 6)
                {
                    CreateMemoryAddress(MemorySegmentType.DungeonItem, i);
                }

                if (i < 2)
                {
                    CreateMemoryAddress(MemorySegmentType.SecondOverworldEvent, i);
                    CreateMemoryAddress(MemorySegmentType.NPCItem, i);
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Subscribes to the StatusChanged event on the ISNESConnector interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="status">
        /// The arguments of the StatusChanged event.
        /// </param>
        private void OnStatusChanged(object? sender, ConnectionStatus status)
        {
            Status = status;
        }

        /// <summary>
        /// Returns the starting address of the specified memory segment.
        /// </summary>
        /// <param name="type">
        /// The memory segment type.
        /// </param>
        /// <returns>
        /// A 64-bit unsigned integer representing the starting memory address of the segment.
        /// </returns>
        private static ulong GetMemorySegmentStart(MemorySegmentType type)
        {
            return type switch
            {
                MemorySegmentType.FirstRoom => 0x7ef000,
                MemorySegmentType.SecondRoom => 0x7ef080,
                MemorySegmentType.ThirdRoom => 0x7ef100,
                MemorySegmentType.FourthRoom => 0x7ef180,
                MemorySegmentType.FifthRoom => 0x7ef200,
                MemorySegmentType.FirstOverworldEvent => 0x7ef280,
                MemorySegmentType.SecondOverworldEvent => 0x7ef300,
                MemorySegmentType.FirstItem => 0x7ef340,
                MemorySegmentType.SecondItem => 0x7ef3c0,
                MemorySegmentType.NPCItem => 0x7ef410,
                MemorySegmentType.DungeonItem => 0x7ef434,
                MemorySegmentType.Dungeon => 0x7ef4c0,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }

        /// <summary>
        /// Creates a memory address for the specified memory segment and offset.
        /// </summary>
        /// <param name="type">
        /// The memory segment type.
        /// </param>
        /// <param name="offset">
        /// The offset of the address.
        /// </param>
        private void CreateMemoryAddress(MemorySegmentType type, ulong offset)
        {
            var memoryAddress = _addressFactory();
            var memorySegment = _memorySegments[type];
            memorySegment.Add(memoryAddress);
            var address = GetMemorySegmentStart(type);
            address += offset;
            MemoryAddresses.Add(address, memoryAddress);
        }
        
        /// <summary>
        /// Updates cached values of a memory segment.
        /// </summary>
        /// <param name="segment">
        /// The segment to be updated.
        /// </param>
        private async Task MemoryCheck(MemorySegmentType segment)
        {
            var memorySegment = _memorySegments[segment];
            var startAddress = GetMemorySegmentStart(segment);
            var buffer = await SNESConnector.Read(startAddress, memorySegment.Count);

            if (!(buffer is null))
            {
                for (var i = 0; i < buffer.Length; i++)
                {
                    memorySegment[i].Value = buffer[i];
                }
            }
        }

        /// <summary>
        /// Returns the list of devices provided by the SNES connector.
        /// </summary>
        /// <returns>
        /// A list of strings representing the devices.
        /// </returns>
        private async Task<List<string>> GetDevicesFromConnector()
        {
            var devices = await SNESConnector.GetDevices();

            if (devices is null)
            {
                return new List<string>();
            }
            
            return new List<string>(devices);
        }

        /// <summary>
        /// Updates cached value of the SNES memory address that provides game status.
        /// </summary>
        public async Task InGameCheck()
        {
            if (CanReadMemory)
            {
                var result = await SNESConnector.Read(0x7e0010);

                if (result is null)
                {
                    return;
                }

                _inGameStatus = result[0];
            }
        }

        /// <summary>
        /// Updates cached values of all SNES memory addresses.
        /// </summary>
        public async Task MemoryCheck()
        {
            if (CanReadMemory && IsInGame)
            {
                foreach (MemorySegmentType segment in Enum.GetValues(typeof(MemorySegmentType)))
                {
                    await MemoryCheck(segment);
                }
            }
        }

        /// <summary>
        /// Returns whether the web socket can be connected to.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the web socket can be connected to.
        /// </returns>
        public bool CanConnect()
        {
            return Status == ConnectionStatus.NotConnected;
        }

        /// <summary>
        /// Connects to the web socket with the specified URI string.
        /// </summary>
        /// <param name="uriString">
        /// A string representing the web socket URI.
        /// </param>
        public async Task Connect(string uriString)
        {
            SNESConnector.SetUri(uriString);
            await SNESConnector.Connect();
        }

        /// <summary>
        /// Returns whether the web socket is able to provide the devices.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the web socket is able to provide the devices.
        /// </returns>
        public bool CanGetDevices()
        {
            return Status == ConnectionStatus.SelectDevice;
        }

        /// <summary>
        /// Updates the list of devices.
        /// </summary>
        public async Task GetDevices()
        {
            Devices = await GetDevicesFromConnector();
        }

        /// <summary>
        /// Returns whether the web socket can be disconnected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the web socket can be disconnected.
        /// </returns>
        public bool CanDisconnect()
        {
            return Status != ConnectionStatus.NotConnected;
        }

        /// <summary>
        /// Disconnects the autotracker.
        /// </summary>
        public async Task Disconnect()
        { 
            await SNESConnector.Disconnect();
            _inGameStatus = null;
            
            foreach (var address in MemoryAddresses.Values)
            {
                address.Reset();
            }
        }

        /// <summary>
        /// Returns whether autotracking can be started.
        /// </summary>
        /// <returns>
        /// A boolean representing whether autotracking can be started.
        /// </returns>
        public bool CanStart()
        {
            return Status == ConnectionStatus.SelectDevice;
        }

        /// <summary>
        /// Starts autotracking.
        /// </summary>
        public async Task Start(string device)
        {
            await SNESConnector.SetDevice(device);
        }
    }
}
