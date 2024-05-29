using System.Runtime.CompilerServices;

namespace BestHTTP.SocketIO
{
	public sealed class Error
	{
		[CompilerGenerated]
		private SocketIOErrors socketIOErrors_0;

		[CompilerGenerated]
		private string string_0;

		public SocketIOErrors SocketIOErrors_0
		{
			[CompilerGenerated]
			get
			{
				return socketIOErrors_0;
			}
			[CompilerGenerated]
			private set
			{
				socketIOErrors_0 = value;
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

		public Error(SocketIOErrors socketIOErrors_1, string string_1)
		{
			SocketIOErrors_0 = socketIOErrors_1;
			String_0 = string_1;
		}

		public override string ToString()
		{
			return string.Format("Code: {0} Message: \"{1}\"", SocketIOErrors_0.ToString(), String_0);
		}
	}
}
