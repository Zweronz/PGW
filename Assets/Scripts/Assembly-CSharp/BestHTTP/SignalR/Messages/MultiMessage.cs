using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BestHTTP.SignalR.Messages
{
	public sealed class MultiMessage : IServerMessage
	{
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private bool bool_1;

		[CompilerGenerated]
		private TimeSpan? nullable_0;

		[CompilerGenerated]
		private List<IServerMessage> list_0;

		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Multiple;
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

		public bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_1;
			}
			[CompilerGenerated]
			private set
			{
				bool_1 = value;
			}
		}

		public TimeSpan? Nullable_0
		{
			[CompilerGenerated]
			get
			{
				return nullable_0;
			}
			[CompilerGenerated]
			private set
			{
				nullable_0 = value;
			}
		}

		public List<IServerMessage> List_0
		{
			[CompilerGenerated]
			get
			{
				return list_0;
			}
			[CompilerGenerated]
			private set
			{
				list_0 = value;
			}
		}

		void IServerMessage.Parse(object data)
		{
			IDictionary<string, object> dictionary = data as IDictionary<string, object>;
			String_0 = dictionary["C"] as string;
			object value;
			if (dictionary.TryGetValue("S", out value))
			{
				Boolean_0 = int.Parse(value.ToString()) == 1;
			}
			else
			{
				Boolean_0 = false;
			}
			if (dictionary.TryGetValue("G", out value))
			{
				String_1 = value as string;
			}
			if (dictionary.TryGetValue("T", out value))
			{
				Boolean_1 = int.Parse(value.ToString()) == 1;
			}
			else
			{
				Boolean_1 = false;
			}
			if (dictionary.TryGetValue("L", out value))
			{
				Nullable_0 = TimeSpan.FromMilliseconds(double.Parse(value.ToString()));
			}
			IEnumerable enumerable = dictionary["M"] as IEnumerable;
			if (enumerable == null)
			{
				return;
			}
			List_0 = new List<IServerMessage>();
			foreach (object item in enumerable)
			{
				IDictionary<string, object> dictionary2 = item as IDictionary<string, object>;
				IServerMessage serverMessage = null;
				serverMessage = ((dictionary2 == null) ? new DataMessage() : ((!dictionary2.ContainsKey("H")) ? ((!dictionary2.ContainsKey("I")) ? ((IServerMessage)new DataMessage()) : ((IServerMessage)new ProgressMessage())) : new MethodCallMessage()));
				serverMessage.Parse(item);
				List_0.Add(serverMessage);
			}
		}
	}
}
