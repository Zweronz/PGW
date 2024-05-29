using System.Runtime.CompilerServices;
using engine.helpers;

namespace engine.network
{
	public class ServerInfo
	{
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private string string_3;

		[CompilerGenerated]
		private string string_4;

		[CompilerGenerated]
		private string string_5;

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			protected set
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
			protected set
			{
				string_1 = value;
			}
		}

		public string String_2
		{
			[CompilerGenerated]
			get
			{
				return string_2;
			}
			[CompilerGenerated]
			protected set
			{
				string_2 = value;
			}
		}

		public string String_3
		{
			[CompilerGenerated]
			get
			{
				return string_3;
			}
			[CompilerGenerated]
			protected set
			{
				string_3 = value;
			}
		}

		public string String_4
		{
			[CompilerGenerated]
			get
			{
				return string_4;
			}
			[CompilerGenerated]
			protected set
			{
				string_4 = value;
			}
		}

		public string String_5
		{
			[CompilerGenerated]
			get
			{
				return string_5;
			}
			[CompilerGenerated]
			protected set
			{
				string_5 = value;
			}
		}

		public ServerInfo(string string_6, string string_7, string string_8, string string_9, string string_10, string string_11)
		{
			String_1 = string_6;
			String_0 = string_7;
			String_2 = string_8;
			String_4 = string_9;
			String_3 = string_10;
			String_5 = string_11;
		}

		public string GetEnterUrl()
		{
			return string.Format("{0}{1}", String_0, String_3);
		}

		public string GetAuthUrl()
		{
			return string.Format("{0}{1}", String_0, String_2);
		}

		public string GetRegistrationUrl()
		{
			return string.Format("{0}{1}", String_0, String_4);
		}

		public string GetSignature(string string_6, string string_7 = "")
		{
			return EncryptionHelper.Md5Sum(string.Format("{0}_{1}_{2}", string_6, string_7, String_5));
		}
	}
}
