using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BestHTTP.SocketIO
{
	public sealed class SocketOptions
	{
		private float float_0;

		private string string_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private TimeSpan timeSpan_0;

		[CompilerGenerated]
		private TimeSpan timeSpan_1;

		[CompilerGenerated]
		private TimeSpan timeSpan_2;

		[CompilerGenerated]
		private bool bool_1;

		[CompilerGenerated]
		private Dictionary<string, string> dictionary_0;

		[CompilerGenerated]
		private bool bool_2;

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			set
			{
				bool_0 = value;
			}
		}

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
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
			set
			{
				timeSpan_0 = value;
			}
		}

		public TimeSpan TimeSpan_1
		{
			[CompilerGenerated]
			get
			{
				return timeSpan_1;
			}
			[CompilerGenerated]
			set
			{
				timeSpan_1 = value;
			}
		}

		public float Single_0
		{
			get
			{
				return float_0;
			}
			set
			{
				float_0 = Math.Min(1f, Math.Max(0f, value));
			}
		}

		public TimeSpan TimeSpan_2
		{
			[CompilerGenerated]
			get
			{
				return timeSpan_2;
			}
			[CompilerGenerated]
			set
			{
				timeSpan_2 = value;
			}
		}

		public bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_1;
			}
			[CompilerGenerated]
			set
			{
				bool_1 = value;
			}
		}

		public Dictionary<string, string> Dictionary_0
		{
			[CompilerGenerated]
			get
			{
				return dictionary_0;
			}
			[CompilerGenerated]
			set
			{
				dictionary_0 = value;
			}
		}

		public bool Boolean_2
		{
			[CompilerGenerated]
			get
			{
				return bool_2;
			}
			[CompilerGenerated]
			set
			{
				bool_2 = value;
			}
		}

		public SocketOptions()
		{
			Boolean_0 = true;
			Int32_0 = int.MaxValue;
			TimeSpan_0 = TimeSpan.FromMilliseconds(1000.0);
			TimeSpan_1 = TimeSpan.FromMilliseconds(5000.0);
			Single_0 = 0.5f;
			TimeSpan_2 = TimeSpan.FromMilliseconds(20000.0);
			Boolean_1 = true;
			Boolean_2 = true;
		}

		internal string BuildQueryParams()
		{
			if (Dictionary_0 != null && Dictionary_0.Count != 0)
			{
				if (!string.IsNullOrEmpty(string_0))
				{
					return string_0;
				}
				StringBuilder stringBuilder = new StringBuilder(Dictionary_0.Count * 4);
				foreach (KeyValuePair<string, string> item in Dictionary_0)
				{
					stringBuilder.Append("&");
					stringBuilder.Append(item.Key);
					if (!string.IsNullOrEmpty(item.Value))
					{
						stringBuilder.Append("=");
						stringBuilder.Append(item.Value);
					}
				}
				return string_0 = stringBuilder.ToString();
			}
			return string.Empty;
		}
	}
}
