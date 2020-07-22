namespace OpenTracker.Models.AutoTracking
{
	/// <summary>
	/// This is the class that translates SNES memory addresses to be used by the USB2SNES
	/// connector.
	/// </summary>
    public static class AddressTranslator
    {
		/// <summary>
		/// Translates a memory address from one range of addresses to another.
		/// </summary>
		/// <param name="address">
		/// An unsigned 32-bit integer representing the untranslated memory address.
		/// </param>
		/// <param name="sourceRangeBegin">
		/// An unsigned 32-bit integer representing the start of the memory address range in which
		/// the untranslated address resides.
		/// </param>
		/// <param name="sourceRangeEnd">
		/// An unsigned 32-bit integer representing the end of the memory address range in which
		/// the untranslated address resides.
		/// </param>
		/// <param name="destinationRangeBegin">
		/// An unsigned 32-bit integer representing the start of the memory address range in which
		/// the translated address will reside.
		/// </param>
		/// <param name="mappedAddress">
		/// An unsigned 32-bit integer representing the output of the translated memory address.
		/// </param>
		/// <returns>
		/// A boolean representing whether the memory address was successfully translated.
		/// </returns>
		private static bool MapAddressInRange(
			uint address, uint sourceRangeBegin, uint sourceRangeEnd,
			uint destinationRangeBegin, out uint mappedAddress)
		{
			if (address >= sourceRangeBegin && address <= sourceRangeEnd)
			{
				mappedAddress = address - sourceRangeBegin + destinationRangeBegin;
				return true;
			}

			mappedAddress = 0U;
			return false;
		}

		/// <summary>
		/// Translates an input address to the SD2SNES address space.
		/// </summary>
		/// <param name="address">
		/// An unsigned 32-bit integer representing the untranslated address.
		/// </param>
		/// <param name="mode">
		/// The translation mode.
		/// </param>
		/// <returns>
		/// An unsigned 32-bit integer representing the translated address.
		/// </returns>
		public static uint TranslateAddress(uint address, TranslationMode mode)
		{
			if (mode == TranslationMode.Read && MapAddressInRange(
				address, 8257536U, 8388607U, 16056320U, out uint mappedAddress))
			{
				return mappedAddress;
			}

			for (uint index = 0; index < 63U; ++index)
			{
				if (MapAddressInRange(address, (uint)((int)index * 65536 + 32768),
					(uint)((int)index * 65536 + ushort.MaxValue),
					index * 32768U, out mappedAddress) ||
					MapAddressInRange(address, (uint)((int)index * 65536 + 8421376),
					(uint)((int)index * 65536 + 8454143),
					index * 32768U, out mappedAddress))
				{
					return mappedAddress;
				}
			}

			for (uint index = 0; index < 8U; ++index)
			{
				if (MapAddressInRange(address, (uint)(7340032 + (int)index * 65536),
					(uint)(7372799 + (int)index * 65536),
					(uint)(14680064 + (int)index * 32768), out mappedAddress))
				{
					return mappedAddress;
				}
			}

			return address;
		}
	}
}
