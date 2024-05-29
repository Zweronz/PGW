using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BestHTTP.SignalR.Messages
{
	public sealed class ProgressMessage : IServerMessage, IHubMessage
	{
		[CompilerGenerated]
		private ulong ulong_0;

		[CompilerGenerated]
		private double double_0;

		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Progress;
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

		public double Double_0
		{
			[CompilerGenerated]
			get
			{
				return double_0;
			}
			[CompilerGenerated]
			private set
			{
				double_0 = value;
			}
		}

		void IServerMessage.Parse(object data)
		{
			IDictionary<string, object> dictionary = data as IDictionary<string, object>;
			IDictionary<string, object> dictionary2 = dictionary["P"] as IDictionary<string, object>;
			UInt64_0 = ulong.Parse(dictionary2["I"] as string);
			Double_0 = double.Parse(dictionary2["D"].ToString());
		}
	}
}
