using System.Text;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	internal static class StringPluginExtensions
	{
		public static readonly char[] ScrambledChars;

		private static int _lastRndSeed;

		static StringPluginExtensions()
		{
			ScrambledChars = new char[25]
			{
				'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
				'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
				'U', 'V', 'X', 'Y', 'Z'
			};
			ScrambledChars.ScrambleChars();
		}

		internal static void ScrambleChars(this char[] chars)
		{
			int num = chars.Length;
			for (int i = 0; i < num; i++)
			{
				char c = chars[i];
				int num2 = Random.Range(i, num);
				chars[i] = chars[num2];
				chars[num2] = c;
			}
		}

		internal static StringBuilder AppendScrambledChars(this StringBuilder buffer, int length, char[] chars)
		{
			if (length <= 0)
			{
				return buffer;
			}
			int num = chars.Length;
			int num2;
			for (num2 = _lastRndSeed; num2 == _lastRndSeed; num2 = Random.Range(0, num))
			{
			}
			_lastRndSeed = num2;
			for (int i = 0; i < length; i++)
			{
				if (num2 >= num)
				{
					num2 = 0;
				}
				buffer.Append(chars[num2]);
				num2++;
			}
			return buffer;
		}
	}
}
