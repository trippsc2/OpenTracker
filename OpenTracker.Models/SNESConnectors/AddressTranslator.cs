namespace OpenTracker.Models.SNESConnectors
{
    internal static class AddressTranslator
    {
		private static bool MapAddressInRange(uint address, uint sourceRangeBegin, uint sourceRangeEnd,
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

		internal static uint TranslateAddress(uint address, TranslationMode mode)
		{
			if (mode == TranslationMode.Read &&
				MapAddressInRange(address, 8257536U, 8388607U, 16056320U, out uint mappedAddress))
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
