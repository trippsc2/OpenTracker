using OpenTracker.Models.AutoTracking.SNESConnectors;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors
{
    public class AddressTranslatorTests
    {
        [Theory]
        [InlineData(0xF5F000U, 0x7EF000U)]
        [InlineData(0x8F00U, 0x18F00U)]
        [InlineData(0xE08F00U, 0x710F00U)]
        [InlineData(0xBF0000U, 0xBF0000U)]
        public void TranslateAddress_ShouldReturnExpected(uint expected, uint address)
        {
            Assert.Equal(expected, AddressTranslator.TranslateAddress(address));
        }
    }
}