namespace OpenTracker.Models.AutoTracking
{
	/// <summary>
	/// This class contains the logic to translate SNES memory addresses to be used by the USB2SNES connector.
	/// </summary>
    public static class AddressTranslator
    {
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
				address, 0x7E0000U, 0x7FFFFFU, 0xF50000U,
				out var mappedAddress))
			{
				return mappedAddress;
			}

			for (uint index = 0x0; index < 0x3FU; ++index)
			{
				if (MapAddressInRange(
					    address, (uint)((int)index * 0x10000 + 0x8000),
					    (uint)((int)index * 0x10000 + ushort.MaxValue),
					    index * 0x8000U, out mappedAddress) ||
				    MapAddressInRange(
					    address, (uint)((int)index * 0x10000 + 0x808000),
					    (uint)((int)index * 0x10000 + 0x80FFFF),
					    index * 0x8000U, out mappedAddress))
				{
					return mappedAddress;
				}
			}

			for (uint index = 0x0; index < 0x8U; ++index)
			{
				if (MapAddressInRange(address, (uint)(0x700000 + (int)index * 0x10000),
					(uint)(0x707FFF + (int)index * 0x10000),
					(uint)(0xE00000 + (int)index * 0x8000), out mappedAddress))
				{
					return mappedAddress;
				}
			}

			return address;
		}

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
			uint address, uint sourceRangeBegin, uint sourceRangeEnd, uint destinationRangeBegin,
			out uint mappedAddress)
		{
			if (address >= sourceRangeBegin && address <= sourceRangeEnd)
			{
				mappedAddress = address - sourceRangeBegin + destinationRangeBegin;
				return true;
			}

			mappedAddress = 0U;
			return false;
		}
	}
}
