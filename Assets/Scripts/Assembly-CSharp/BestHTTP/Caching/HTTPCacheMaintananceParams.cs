using System;
using System.Runtime.CompilerServices;

namespace BestHTTP.Caching
{
	public sealed class HTTPCacheMaintananceParams
	{
		[CompilerGenerated]
		private TimeSpan timeSpan_0;

		[CompilerGenerated]
		private ulong ulong_0;

		public TimeSpan TimeSpan_0
		{
			[CompilerGenerated]
			get
			{
				return timeSpan_0;
			}
			[CompilerGenerated]
			private set
			{
				timeSpan_0 = value;
			}
		}

		public ulong UInt64_0
		{
			[CompilerGenerated]
			get
			{
				return ulong_0;
			}
			[CompilerGenerated]
			private set
			{
				ulong_0 = value;
			}
		}

		public HTTPCacheMaintananceParams(TimeSpan timeSpan_1, ulong ulong_1)
		{
			TimeSpan_0 = timeSpan_1;
			UInt64_0 = ulong_1;
		}
	}
}
