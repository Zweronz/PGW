using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BestHTTP.SignalR.Messages
{
	public sealed class FailureMessage : IServerMessage, IHubMessage
	{
		[CompilerGenerated]
		private ulong ulong_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private IDictionary<string, object> idictionary_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private IDictionary<string, object> idictionary_1;

		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Failure;
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

		public bool Boolean_0
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			private set
			{
				bool_0 = value;
			}
		}

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			private set
			{
				string_0 = value;
			}
		}

		public IDictionary<string, object> IDictionary_0
		{
			[CompilerGenerated]
			get
			{
				return idictionary_0;
			}
			[CompilerGenerated]
			private set
			{
				idictionary_0 = value;
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
			private set
			{
				string_1 = value;
			}
		}

		public IDictionary<string, object> IDictionary_1
		{
			[CompilerGenerated]
			get
			{
				return idictionary_1;
			}
			[CompilerGenerated]
			private set
			{
				idictionary_1 = value;
			}
		}

		void IServerMessage.Parse(object data)
		{
			IDictionary<string, object> dictionary = data as IDictionary<string, object>;
			UInt64_0 = ulong.Parse(dictionary["I"] as string);
			object value;
			if (dictionary.TryGetValue("E", out value))
			{
				String_0 = value as string;
			}
			if (dictionary.TryGetValue("H", out value))
			{
				Boolean_0 = int.Parse(value.ToString()) == 1;
			}
			if (dictionary.TryGetValue("D", out value))
			{
				IDictionary_0 = value as IDictionary<string, object>;
			}
			if (dictionary.TryGetValue("T", out value))
			{
				String_1 = value as string;
			}
			if (dictionary.TryGetValue("S", out value))
			{
				IDictionary_1 = value as IDictionary<string, object>;
			}
		}
	}
}
