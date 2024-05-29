using System.Runtime.CompilerServices;
using System.Text;

namespace BestHTTP.Forms
{
	public class HTTPFieldData
	{
		[CompilerGenerated]
		private string string_0;

		[CompilerGenerated]
		private string string_1;

		[CompilerGenerated]
		private string string_2;

		[CompilerGenerated]
		private Encoding encoding_0;

		[CompilerGenerated]
		private string string_3;

		[CompilerGenerated]
		private byte[] byte_0;

		public string String_0
		{
			[CompilerGenerated]
			get
			{
				return string_0;
			}
			[CompilerGenerated]
			set
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
			set
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
			set
			{
				string_2 = value;
			}
		}

		public Encoding Encoding_0
		{
			[CompilerGenerated]
			get
			{
				return encoding_0;
			}
			[CompilerGenerated]
			set
			{
				encoding_0 = value;
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
			set
			{
				string_3 = value;
			}
		}

		public byte[] Byte_0
		{
			[CompilerGenerated]
			get
			{
				return byte_0;
			}
			[CompilerGenerated]
			set
			{
				byte_0 = value;
			}
		}

		public byte[] Byte_1
		{
			get
			{
				if (Byte_0 != null)
				{
					return Byte_0;
				}
				if (Encoding_0 == null)
				{
					Encoding_0 = Encoding.UTF8;
				}
				return Byte_0 = Encoding_0.GetBytes(String_3);
			}
		}
	}
}
