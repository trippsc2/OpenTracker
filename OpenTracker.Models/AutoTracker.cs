using OpenTracker.Models.AutotrackerConnectors;
using OpenTracker.Models.Enums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTracker.Models
{
    public class AutoTracker : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private CancellationTokenSource _tokenSource;
        private CancellationToken? _token;

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private USB2SNESConnector _connector;
        public USB2SNESConnector Connector
        {
            get => _connector;
            set
            {
                if (_connector != value)
                {
                    OnPropertyChanging(nameof(Connector));
                    _connector = value;
                    OnPropertyChanged(nameof(Connector));
                }
            }
        }

        private byte? _inGameStatus;
        public byte? InGameStatus
        {
            get => _inGameStatus;
            private set
            {
                if (_inGameStatus != value)
                {
                    _inGameStatus = value;
                    OnPropertyChanged(nameof(InGameStatus));
                }
            }
        }

        public ObservableCollection<byte> RoomMemory { get; }
        public ObservableCollection<byte> OverworldEventMemory { get; }
        public ObservableCollection<byte> ItemMemory { get; }
        public ObservableCollection<byte> NPCItemMemory { get; }

        public AutoTracker()
        {
            RoomMemory = new ObservableCollection<byte>();
            OverworldEventMemory = new ObservableCollection<byte>();
            ItemMemory = new ObservableCollection<byte>();
            NPCItemMemory = new ObservableCollection<byte>();
        }

        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(InGameStatus))
                return;
        }

        public void Start(Action<string> messageHandler)
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            Connector = new USB2SNESConnector(messageHandler);

            int i = 0;

            while (!Connector.Connected && i <= 5)
            {
                Thread.Sleep(1000);
                i++;
            }

            if (Connector.Connected)
            {
                MemoryCheck(() =>
                {
                    _connector.ReadByte(0x7e0010, out byte inGameStatus);
                    InGameStatus = inGameStatus;

                    byte[] buffer = new byte[592];
                    _connector.Read(0x7ef000, buffer);

                    RoomMemory.Clear();

                    foreach (byte value in buffer)
                        RoomMemory.Add(value);

                    buffer = new byte[130];
                    _connector.Read(0x7ef280, buffer);

                    OverworldEventMemory.Clear();

                    foreach (byte value in buffer)
                        OverworldEventMemory.Add(value);

                    buffer = new byte[144];
                    _connector.Read(0x7ef340, buffer);

                    ItemMemory.Clear();

                    foreach (byte value in buffer)
                        ItemMemory.Add(value);

                    buffer = new byte[2];
                    _connector.Read(0x7ef410, buffer);

                    NPCItemMemory.Clear();

                    foreach (byte value in buffer)
                        NPCItemMemory.Add(value);
                }, TimeSpan.FromMilliseconds(5000), _token.Value);
            }
        }

        public void Stop()
        {
            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
                _tokenSource = null;
                _token = null;
            }

            Connector.Dispose();
            Connector = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            InGameStatus = null;
            ItemMemory.Clear();
        }

        public bool IsInGame()
        {
            if (InGameStatus != null && InGameStatus.Value > 0x05 && InGameStatus.Value != 0x14)
                return true;

            return false;
        }

        private static void MemoryCheck(Action action, TimeSpan interval, CancellationToken token)
        {
            if (action != null)
            {
                Task.Run(async () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        try { action(); }
                        catch { }
                        
                        await Task.Delay(interval, token);
                    }
                }, token);
            }
        }

        public bool? CheckMemoryFlag(MemorySegmentType segment, int index, byte flag)
        {
            ObservableCollection<byte> memory = null;

            switch (segment)
            {
                case MemorySegmentType.Room:
                    memory = RoomMemory;
                    break;
                case MemorySegmentType.OverworldEvent:
                    memory = OverworldEventMemory;
                    break;
                case MemorySegmentType.Item:
                    memory = ItemMemory;
                    break;
                case MemorySegmentType.NPCItem:
                    memory = NPCItemMemory;
                    break;
            }

            if (memory == null)
                throw new ArgumentOutOfRangeException(nameof(segment));

            if (memory.Count > index)
            {
                if ((memory[index] & flag) != 0)
                    return true;
                else
                    return false;
            }
            
            return null;
        }

        public int? CheckMemoryFlagArray((MemorySegmentType, int, byte)[] addressFlags)
        {
            int? result = null;

            foreach ((MemorySegmentType, int, byte) address in addressFlags)
            {
                bool? addressResult = CheckMemoryFlag(address.Item1, address.Item2, address.Item3);

                if (addressResult.HasValue)
                {
                    if (addressResult.Value)
                    {
                        if (result.HasValue)
                            result++;
                        else
                            result = 1;
                    }
                    else if (!result.HasValue)
                        result = 0;
                }
            }

            return result;
        }

        public bool? CheckMemoryByte(MemorySegmentType segment, int index, byte minValue = 0)
        {
            ObservableCollection<byte> memory = null;

            switch (segment)
            {
                case MemorySegmentType.Room:
                    memory = RoomMemory;
                    break;
                case MemorySegmentType.OverworldEvent:
                    memory = OverworldEventMemory;
                    break;
                case MemorySegmentType.Item:
                    memory = ItemMemory;
                    break;
                case MemorySegmentType.NPCItem:
                    memory = NPCItemMemory;
                    break;
            }

            if (memory == null)
                throw new ArgumentOutOfRangeException(nameof(segment));

            if (memory.Count > index)
            {
                if (memory[index] > minValue)
                    return true;
                else
                    return false;
            }

            return null;
        }

        public byte? CheckMemoryByteValue(MemorySegmentType segment, int index)
        {
            ObservableCollection<byte> memory = null;

            switch (segment)
            {
                case MemorySegmentType.Room:
                    memory = RoomMemory;
                    break;
                case MemorySegmentType.OverworldEvent:
                    memory = OverworldEventMemory;
                    break;
                case MemorySegmentType.Item:
                    memory = ItemMemory;
                    break;
                case MemorySegmentType.NPCItem:
                    memory = NPCItemMemory;
                    break;
            }

            if (memory == null)
                throw new ArgumentOutOfRangeException(nameof(segment));

            if (memory.Count > index)
                return memory[index];

            return null;
        }

        public int? CheckMemoryByteArray((MemorySegmentType, int)[] addresses)
        {
            int? result = null;

            foreach ((MemorySegmentType, int) address in addresses)
            {
                bool? addressResult = CheckMemoryByte(address.Item1, address.Item2);

                if (addressResult.HasValue)
                {
                    if (addressResult.Value)
                    {
                        if (result.HasValue)
                            result++;
                        else
                            result = 1;
                    }
                    else if (!result.HasValue)
                        result = 0;
                }
            }

            return result;
        }
    }
}
