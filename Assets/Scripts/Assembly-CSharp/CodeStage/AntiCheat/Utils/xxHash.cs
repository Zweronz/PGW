namespace CodeStage.AntiCheat.Utils
{
	internal class xxHash
	{
		private const uint uint_0 = 2654435761u;

		private const uint uint_1 = 2246822519u;

		private const uint uint_2 = 3266489917u;

		private const uint uint_3 = 668265263u;

		private const uint uint_4 = 374761393u;

		public static uint CalculateHash(byte[] byte_0, int int_0, uint uint_5 = 0)
		{
			int i = 0;
			uint num7;
			if (int_0 >= 16)
			{
				int num = int_0 - 16;
				uint num2 = (uint)((int)uint_5 + -1640531535 + -2048144777);
				uint num3 = uint_5 + 2246822519u;
				uint num4 = uint_5;
				uint num5 = uint_5 - 2654435761u;
				do
				{
					uint num6 = (uint)(byte_0[i++] | (byte_0[i++] << 8) | (byte_0[i++] << 16) | (byte_0[i++] << 24));
					num2 += (uint)((int)num6 * -2048144777);
					num2 = (num2 << 13) | (num2 >> 19);
					num2 *= 2654435761u;
					num6 = (uint)(byte_0[i++] | (byte_0[i++] << 8) | (byte_0[i++] << 16) | (byte_0[i++] << 24));
					num3 += (uint)((int)num6 * -2048144777);
					num3 = (num3 << 13) | (num3 >> 19);
					num3 *= 2654435761u;
					num6 = (uint)(byte_0[i++] | (byte_0[i++] << 8) | (byte_0[i++] << 16) | (byte_0[i++] << 24));
					num4 += (uint)((int)num6 * -2048144777);
					num4 = (num4 << 13) | (num4 >> 19);
					num4 *= 2654435761u;
					num6 = (uint)(byte_0[i++] | (byte_0[i++] << 8) | (byte_0[i++] << 16) | (byte_0[i++] << 24));
					num5 += (uint)((int)num6 * -2048144777);
					num5 = (num5 << 13) | (num5 >> 19);
					num5 *= 2654435761u;
				}
				while (i <= num);
				num7 = ((num2 << 1) | (num2 >> 31)) + ((num3 << 7) | (num3 >> 25)) + ((num4 << 12) | (num4 >> 20)) + ((num5 << 18) | (num5 >> 14));
			}
			else
			{
				num7 = uint_5 + 374761393;
			}
			num7 += (uint)int_0;
			while (i <= int_0 - 4)
			{
				num7 += (uint)((byte_0[i++] | (byte_0[i++] << 8) | (byte_0[i++] << 16) | (byte_0[i++] << 24)) * -1028477379);
				num7 = ((num7 << 17) | (num7 >> 15)) * 668265263;
			}
			for (; i < int_0; i++)
			{
				num7 += (uint)(byte_0[i] * 374761393);
				num7 = ((num7 << 11) | (num7 >> 21)) * 2654435761u;
			}
			num7 ^= num7 >> 15;
			num7 *= 2246822519u;
			num7 ^= num7 >> 13;
			num7 *= 3266489917u;
			return num7 ^ (num7 >> 16);
		}
	}
}
