namespace OpenTracker.Models.AutoTracking.SNESConnectors;

/// <summary>
///	Represents the logic to translate raw SNES memory addresses to be used by the USB2SNES connector.
/// </summary>
public static class AddressTranslator
{
	///  <summary>
	/// Translates an input address to the SD2SNES address space.
	///  </summary>
	///  <param name="address">
	///		A <see cref="int"/> representing the untranslated address.
	///  </param>
	///  <returns>
	/// 	A <see cref="uint"/> representing the translated address.
	///  </returns>
	public static uint TranslateAddress(uint address)
	{
		if (MapAddressInRange(
			    address, 0x7E0000U, 0x7FFFFFU, 0xF50000U,
			    out var mappedAddress))
		{
			return mappedAddress;
		}

		for (uint index = 0x0; index < 0x3F; ++index)
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
	///	Translates a memory address from one range of addresses to another.
	/// </summary>
	/// <param name="address">
	///		A <see cref="uint"/> representing the untranslated memory address.
	/// </param>
	/// <param name="sourceRangeBegin">
	///		A <see cref="uint"/> representing the start of the memory address range in which the untranslated
	///		address resides.
	/// </param>
	/// <param name="sourceRangeEnd">
	///		A <see cref="uint"/> representing the end of the memory address range in which the untranslated address
	///		resides.
	/// </param>
	/// <param name="destinationRangeBegin">
	///		A <see cref="uint"/> representing the start of the memory address range in which the translated
	///		address will reside.
	/// </param>
	/// <param name="mappedAddress">
	///		A <see cref="uint"/> representing the output of the translated memory address.
	/// </param>
	/// <returns>
	///		A <see cref="bool"/> representing whether the memory address was successfully translated.
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