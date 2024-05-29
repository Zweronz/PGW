using System;
using System.Runtime.CompilerServices;

namespace BestHTTP.ServerSentEvents
{
	public sealed class Message
	{
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private TimeSpan timeSpan_0;

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			internal set
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
			internal set
			{
				string_1 = value;
			}
		}

		public string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			internal set
			{
				string_2 = value;
			}
		}

		public TimeSpan TimeSpan_0
		{
			[CompilerGenerated]
			get
			{
				return timeSpan_0;
			}
			[CompilerGenerated]
			internal set
			{
				timeSpan_0 = value;
			}
		}

		public override string ToString()
		{
			return string.Format("\"{0}\": \"{1}\"", String_1, String_2);
		}
	}
}
