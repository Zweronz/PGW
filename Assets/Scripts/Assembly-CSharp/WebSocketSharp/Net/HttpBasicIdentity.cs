using System.Security.Principal;

namespace WebSocketSharp.Net
{
	public class HttpBasicIdentity : GenericIdentity
	{
		private string string_0;

		public virtual string String_0
		{
			get
			{
				return string_0;
			}
		}

		internal HttpBasicIdentity(string string_1, string string_2)
			: base(string_1, "Basic")
		{
			string_0 = string_2;
		}
	}
}
