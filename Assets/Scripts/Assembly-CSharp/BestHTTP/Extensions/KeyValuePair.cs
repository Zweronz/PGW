using System.Runtime.CompilerServices;

namespace BestHTTP.Extensions
{
	public sealed class KeyValuePair
	{
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			set
			{
				string_0 = value;
			}
		}

		public string String_1
		{
			[CompilerGenerated]
			get
			{
				return string_1;
			}
			[CompilerGenerated]
			set
			{
				string_1 = value;
			}
		}

		public KeyValuePair(string string_2)
		{
			String_0 = string_2;
		}

		public override string ToString()
		{
			if (!string.IsNullOrEmpty(String_1))
			{
				return String_0 + '=' + String_1;
			}
			return String_0;
		}
	}
}
