using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking.Memory;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Memory
{
    public class MemoryFlagTests
    {
        private readonly IMemoryAddress _memoryAddress = Substitute.For<IMemoryAddress>();
        
        [Theory]
        [InlineData(null, null, 0x1)]
        [InlineData(false, (byte)0, 0x1)]
        [InlineData(true, (byte)1, 0x1)]
        [InlineData(false, (byte)254, 0x1)]
        [InlineData(true, (byte)255, 0x1)]
        [InlineData(null, null, 0x2)]
        [InlineData(false, (byte)0, 0x2)]
        [InlineData(false, (byte)1, 0x2)]
        [InlineData(true, (byte)2, 0x2)]
        [InlineData(false, (byte)253, 0x2)]
        [InlineData(true, (byte)254, 0x2)]
        [InlineData(true, (byte)255, 0x2)]
        [InlineData(null, null, 0x4)]
        [InlineData(false, (byte)0, 0x4)]
        [InlineData(false, (byte)1, 0x4)]
        [InlineData(false, (byte)2, 0x4)]
        [InlineData(false, (byte)3, 0x4)]
        [InlineData(true, (byte)4, 0x4)]
        [InlineData(false, (byte)251, 0x4)]
        [InlineData(true, (byte)252, 0x4)]
        [InlineData(true, (byte)253, 0x4)]
        [InlineData(true, (byte)254, 0x4)]
        [InlineData(true, (byte)255, 0x4)]
        public void Status_ShouldReturnExpected(bool? expected, byte? value, byte flag)
        {
            _memoryAddress.Value.Returns(value);
            var sut = new MemoryFlag(_memoryAddress, flag);
            
            Assert.Equal(expected, sut.Status);
        }

        [Fact]
        public void Status_ShouldRaisePropertyChanged()
        {
            _memoryAddress.Value.Returns((byte?)null);
            var sut = new MemoryFlag(_memoryAddress, 0x1);
            _memoryAddress.Value.Returns((byte?)0);
            
            Assert.PropertyChanged(sut, nameof(IMemoryFlag.Status), () => 
                _memoryAddress.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _memoryAddress, new PropertyChangedEventArgs(nameof(IMemoryAddress.Value))));
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IMemoryFlag.Factory>();
            var sut = factory(_memoryAddress, 0x1);
            
            Assert.NotNull(sut as MemoryFlag);
        }
    }
}