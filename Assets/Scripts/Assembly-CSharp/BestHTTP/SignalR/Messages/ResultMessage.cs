using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BestHTTP.SignalR.Messages
{
	public sealed class ResultMessage : IServerMessage, IHubMessage
	{
		[CompilerGenerated]
		private ulong ulong_0;

		[CompilerGenerated]
		private object object_0;

		[CompilerGenerated]
		private IDictionary<string, object> idictionary_0;

		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Result;
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

		public object Object_0
		{
			[CompilerGenerated]
			get
			{
				return object_0;
			}
			[CompilerGenerated]
			private set
			{
				object_0 = value;
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

		void IServerMessage.Parse(object data)
		{
			IDictionary<string, object> dictionary = data as IDictionary<string, object>;
			UInt64_0 = ulong.Parse(dictionary["I"] as string);
			object value;
			if (dictionary.TryGetValue("R", out value))
			{
				Object_0 = value;
			}
			if (dictionary.TryGetValue("S", out value))
			{
				IDictionary_0 = value as IDictionary<string, object>;
			}
		}
	}
}
