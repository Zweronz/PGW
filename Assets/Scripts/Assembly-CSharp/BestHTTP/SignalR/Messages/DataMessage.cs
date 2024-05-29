using System.Runtime.CompilerServices;

namespace BestHTTP.SignalR.Messages
{
	public sealed class DataMessage : IServerMessage
	{
		[CompilerGenerated]
		private object object_0;

		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Data;
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

		void IServerMessage.Parse(object data)
		{
			Object_0 = data;
		}
	}
}
