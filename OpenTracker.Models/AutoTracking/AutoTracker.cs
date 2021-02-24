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

        private bool InGame =>
            _inGameStatus.HasValue && _inGameStatus.Value > 0x05 && _inGameStatus.Value != 0x14 &&
            _inGameStatus.Value < 0x20;

        public Action<LogLevel, string>? LogHandler { get; set; }

        public ISNESConnector SNESConnector { get; }
        public Dictionary<ulong, IMemoryAddress> MemoryAddresses { get; } =
            new Dictionary<ulong, IMemoryAddress>();

        public event PropertyChangedEventHandler? PropertyChanged;

        private IEnumerable<string>? _devices = new List<string>();
        public IEnumerable<string>? Devices
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
                PropertyChanged?.Invoke(
                    this, new PropertyChangedEventArgs(nameof(RaceIllegalTracking)));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="snesConnector">
        /// The SNES connector.
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

            for (ulong i = 0; i < 592; i++)
            {
                CreateMemoryAddress(MemorySegmentType.Room, i);

                if (i < 130)
                {
                    CreateMemoryAddress(MemorySegmentType.OverworldEvent, i);
                }

                if (i < 144)
                {
                    CreateMemoryAddress(MemorySegmentType.Item, i);
                }

                if (i < 48)
                {
                    CreateMemoryAddress(MemorySegmentType.Dungeon, i);
                }

                if (i < 6)
                {
                    CreateMemoryAddress(MemorySegmentType.DungeonItem, i);
                }

                if (i < 2)
                {
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
                MemorySegmentType.Room => 0x7ef000,
                MemorySegmentType.OverworldEvent => 0x7ef280,
                MemorySegmentType.Item => 0x7ef340,
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
        /// Returns whether the SNES connector is capable of reading memory.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the SNES connector is capable of reading memory.
        /// </returns>
        private bool CanReadMemory()
        {
            return SNESConnector.Socket != null && SNESConnector.Status != ConnectionStatus.Error;
        }

        /// <summary>
        /// Updates cached values of a memory segment.
        /// </summary>
        /// <param name="segment">
        /// The segment to be updated.
        /// </param>
        private void MemoryCheck(MemorySegmentType segment)
        {
            var memorySegment = _memorySegments[segment];
            var startAddress = GetMemorySegmentStart(segment);
            byte[] buffer = new byte[memorySegment.Count];

            if (SNESConnector.Read(startAddress, buffer))
            {
                for (int i = 0; i < buffer.Length; i++)
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
        private async Task<IEnumerable<string>?> GetDevicesFromConnector()
        {
            return await SNESConnector.GetDevices();
        }

        /// <summary>
        /// Updates cached value of the SNES memory address that provides game status.
        /// </summary>
        public void InGameCheck()
        {
            if (CanReadMemory() && SNESConnector.Read(0x7e0010, out byte inGameStatus))
            {
                _inGameStatus = inGameStatus;
            }
        }

        /// <summary>
        /// Updades cached values of all SNES memory addresses.
        /// </summary>
        public void MemoryCheck()
        {
            if (CanReadMemory() && InGame)
            {
                foreach (MemorySegmentType segment in Enum.GetValues(typeof(MemorySegmentType)))
                {
                    MemoryCheck(segment);
                }
            }
        }

        /// <summary>
        /// Updates the list of devices.
        /// </summary>
        public async Task GetDevices()
        {
            Devices = await GetDevicesFromConnector();
        }

        /// <summary>
        /// Stops the autotracker.
        /// </summary>
        public async Task Stop()
        {
            await Task.Factory.StartNew(() =>
            {
                SNESConnector.Disconnect();
                _inGameStatus = null;

                foreach (var address in MemoryAddresses.Values)
                {
                    address.Reset();
                }
            });
        }
    }
}
