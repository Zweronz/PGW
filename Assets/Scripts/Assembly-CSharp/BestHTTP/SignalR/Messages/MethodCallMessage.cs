using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BestHTTP.SignalR.Messages
{
	public sealed class MethodCallMessage : IServerMessage
	{
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private object[] object_0;

		[CompilerGenerated]
		private IDictionary<string, object> idictionary_0;

		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.MethodCall;
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

		public object[] Object_0
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
			String_0 = dictionary["H"] as string;
			String_1 = dictionary["M"] as string;
			List<object> list = new List<object>();
			foreach (object item in dictionary["A"] as IEnumerable)
			{
				list.Add(item);
			}
			Object_0 = list.ToArray();
			object value;
			if (dictionary.TryGetValue("S", out value))
			{
				IDictionary_0 = value as IDictionary<string, object>;
			}
		}
	}
}
