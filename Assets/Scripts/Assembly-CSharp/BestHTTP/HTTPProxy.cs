using System;
using System.Runtime.CompilerServices;
using BestHTTP.Authentication;

namespace BestHTTP
{
	public sealed class HTTPProxy
	{
		[CompilerGenerated]
		private Uri uri_0;

		[CompilerGenerated]
		private Credentials credentials_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private bool bool_1;

		[CompilerGenerated]
		private bool bool_2;

		public Uri Uri_0
		{
			[CompilerGenerated]
			get
			{
				return uri_0;
			}
			[CompilerGenerated]
			set
			{
				uri_0 = value;
			}
		}

		public Credentials Credentials_0
		{
			[CompilerGenerated]
			get
			{
				return credentials_0;
			}
			[CompilerGenerated]
			set
			{
				credentials_0 = value;
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
			set
			{
				bool_0 = value;
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
			set
			{
				bool_1 = value;
			}
		}

		public bool Boolean_2
		{
			[CompilerGenerated]
			get
			{
				return bool_2;
			}
			[CompilerGenerated]
			set
			{
				bool_2 = value;
			}
		}

		public HTTPProxy()
			: this(null, null, false)
		{
		}

		public HTTPProxy(Uri uri_1)
			: this(uri_1, null, false)
		{
		}

		public HTTPProxy(Uri uri_1, Credentials credentials_1)
			: this(uri_1, credentials_1, false)
		{
		}

		public HTTPProxy(Uri uri_1, Credentials credentials_1, bool bool_3)
			: this(uri_1, credentials_1, bool_3, true)
		{
		}

		public HTTPProxy(Uri uri_1, Credentials credentials_1, bool bool_3, bool bool_4)
			: this(uri_1, credentials_1, bool_3, true, true)
		{
		}

		public HTTPProxy(Uri uri_1, Credentials credentials_1, bool bool_3, bool bool_4, bool bool_5)
		{
			Uri_0 = uri_1;
			Credentials_0 = credentials_1;
			Boolean_0 = bool_3;
			Boolean_1 = bool_4;
			Boolean_2 = bool_5;
		}
	}
}
