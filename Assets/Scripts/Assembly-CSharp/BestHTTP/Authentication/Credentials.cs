using System.Runtime.CompilerServices;

namespace BestHTTP.Authentication
{
	public sealed class Credentials
	{
		[CompilerGenerated]
		private AuthenticationTypes authenticationTypes_0;

		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		public AuthenticationTypes AuthenticationTypes_0
		{
			[CompilerGenerated]
			get
			{
				return authenticationTypes_0;
			}
			[CompilerGenerated]
			private set
			{
				authenticationTypes_0 = value;
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

		public Credentials(string string_2, string string_3)
			: this(AuthenticationTypes.Unknown, string_2, string_3)
		{
		}

		public Credentials(AuthenticationTypes authenticationTypes_1, string string_2, string string_3)
		{
			AuthenticationTypes_0 = authenticationTypes_1;
			String_0 = string_2;
			String_1 = string_3;
		}
	}
}
