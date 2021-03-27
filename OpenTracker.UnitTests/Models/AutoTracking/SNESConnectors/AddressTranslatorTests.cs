using OpenTracker.Models.AutoTracking.SNESConnectors;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors
{
    public class AddressTranslatorTests
    {
        [Theory]
        [InlineData(0xF5F000U, 0x7EF000U, TranslationMode.Read)]
        [InlineData(0x7EF000U, 0x7EF000U, TranslationMode.Write)]
        [InlineData(0x8F00U, 0x18F00U, TranslationMode.Read)]
        [InlineData(0x8F00U, 0x18F00U, TranslationMode.Write)]
        [InlineData(0xE08F00U, 0x710F00U, TranslationMode.Read)]
        [InlineData(0xE08F00U, 0x710F00U, TranslationMode.Write)]
        public void TranslateAddress_ShouldReturnExpected(uint expected, uint address, TranslationMode mode)
        {
            Assert.Equal(expected, AddressTranslator.TranslateAddress(address, mode));
        }
    }
}