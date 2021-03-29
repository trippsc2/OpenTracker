using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.AutoTracking.Values.Single;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Values.Single
{
    public class AutoTrackBitwiseIntegerValueTests
    {
        private readonly IMemoryAddress _memoryAddress = Substitute.For<IMemoryAddress>();
        
        [Theory]
        [InlineData(null, null, 0, 0)]
        [InlineData(0, (byte)0, 0, 0)]
        [InlineData(0, (byte)1, 0, 0)]
        [InlineData(0, (byte)255, 0, 0)]
        [InlineData(null, null, 1, 0)]
        [InlineData(0, (byte)0, 1, 0)]
        [InlineData(1, (byte)1, 1, 0)]
        [InlineData(0, (byte)254, 1, 0)]
        [InlineData(1, (byte)255, 1, 0)]
        [InlineData(null, null, 3, 0)]
        [InlineData(0, (byte)0, 3, 0)]
        [InlineData(1, (byte)1, 3, 0)]
        [InlineData(2, (byte)2, 3, 0)]
        [InlineData(3, (byte)3, 3, 0)]
        [InlineData(0, (byte)252, 3, 0)]
        [InlineData(1, (byte)253, 3, 0)]
        [InlineData(2, (byte)254, 3, 0)]
        [InlineData(3, (byte)255, 3, 0)]
        [InlineData(null, null, 0, 1)]
        [InlineData(0, (byte)0, 0, 1)]
        [InlineData(0, (byte)1, 0, 1)]
        [InlineData(0, (byte)255, 0, 1)]
        [InlineData(null, null, 1, 1)]
        [InlineData(0, (byte)0, 1, 1)]
        [InlineData(0, (byte)255, 1, 1)]
        [InlineData(null, null, 3, 1)]
        [InlineData(0, (byte)0, 3, 1)]
        [InlineData(0, (byte)1, 3, 1)]
        [InlineData(1, (byte)2, 3, 1)]
        [InlineData(1, (byte)3, 3, 1)]
        [InlineData(0, (byte)252, 3, 1)]
        [InlineData(0, (byte)253, 3, 1)]
        [InlineData(1, (byte)254, 3, 1)]
        [InlineData(1, (byte)255, 3, 1)]
        public void CurrentValue_ShouldEqualExpected(int? expected, byte? memoryAddressValue, byte mask, int shift)
        {
            _memoryAddress.Value.Returns(memoryAddressValue);
            var sut = new AutoTrackBitwiseIntegerValue(_memoryAddress, mask, shift);
            
            Assert.Equal(expected, sut.CurrentValue);
        }

        [Fact]
        public void MemoryAddressChanged_ShouldRaisePropertyChanged()
        {
            _memoryAddress.Value.Returns((byte?)null);
            var sut = new AutoTrackBitwiseIntegerValue(_memoryAddress, 255, 0);
            _memoryAddress.Value.Returns((byte?)1);
            
            Assert.PropertyChanged(sut, nameof(IAutoTrackValue.CurrentValue),
                () => _memoryAddress.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _memoryAddress, new PropertyChangedEventArgs(nameof(IMemoryAddress.Value))));
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IAutoTrackBitwiseIntegerValue.Factory>();
            var sut = factory(_memoryAddress, byte.MaxValue, 0);
            
            Assert.NotNull(sut as AutoTrackBitwiseIntegerValue);
        }
    }
}