using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BestHTTP.Forms
{
	public class HTTPFormBase
	{
		private const int int_0 = 256;

		[CompilerGenerated]
		private List<HTTPFieldData> list_0;

		[CompilerGenerated]
		private bool bool_0;

		[CompilerGenerated]
		private bool bool_1;

		[CompilerGenerated]
		private bool bool_2;

		public List<HTTPFieldData> List_0
		{
			[CompilerGenerated]
			get
			{
				return list_0;
			}
			[CompilerGenerated]
			set
			{
				list_0 = value;
			}
		}

		public bool Boolean_0
		{
			get
			{
				return List_0 == null || List_0.Count == 0;
			}
		}

		public bool Boolean_1
		{
			[CompilerGenerated]
			get
			{
				return bool_0;
			}
			[CompilerGenerated]
			protected set
			{
				bool_0 = value;
			}
		}

		public bool Boolean_2
		{
			[CompilerGenerated]
			get
			{
				return bool_1;
			}
			[CompilerGenerated]
			protected set
			{
				bool_1 = value;
			}
		}

		public bool Boolean_3
		{
			[CompilerGenerated]
			get
			{
				return bool_2;
			}
			[CompilerGenerated]
			protected set
			{
				bool_2 = value;
			}
		}

		public void AddBinaryData(string string_0, byte[] byte_0)
		{
			AddBinaryData(string_0, byte_0, null, null);
		}

		public void AddBinaryData(string string_0, byte[] byte_0, string string_1)
		{
			AddBinaryData(string_0, byte_0, string_1, null);
		}

		public void AddBinaryData(string string_0, byte[] byte_0, string string_1, string string_2)
		{
			if (List_0 == null)
			{
				List_0 = new List<HTTPFieldData>();
			}
			HTTPFieldData hTTPFieldData = new HTTPFieldData();
			hTTPFieldData.String_0 = string_0;
			if (string_1 == null)
			{
				hTTPFieldData.String_1 = string_0 + ".dat";
			}
			else
			{
				hTTPFieldData.String_1 = string_1;
			}
			if (string_2 == null)
			{
				hTTPFieldData.String_2 = "application/octet-stream";
			}
			else
			{
				hTTPFieldData.String_2 = string_2;
			}
			hTTPFieldData.Byte_0 = byte_0;
			List_0.Add(hTTPFieldData);
			Boolean_1 = true;
			Boolean_2 = true;
		}

		public void AddField(string string_0, string string_1)
		{
			AddField(string_0, string_1, Encoding.UTF8);
		}

		public void AddField(string string_0, string string_1, Encoding encoding_0)
		{
			if (List_0 == null)
			{
				List_0 = new List<HTTPFieldData>();
			}
			HTTPFieldData hTTPFieldData = new HTTPFieldData();
			hTTPFieldData.String_0 = string_0;
			hTTPFieldData.String_1 = null;
			hTTPFieldData.String_2 = "text/plain; charset=\"" + encoding_0.WebName + "\"";
			hTTPFieldData.String_3 = string_1;
			hTTPFieldData.Encoding_0 = encoding_0;
			List_0.Add(hTTPFieldData);
			Boolean_1 = true;
			Boolean_3 |= string_1.Length > 256;
		}

		public virtual void CopyFrom(HTTPFormBase httpformBase_0)
		{
			List_0 = new List<HTTPFieldData>(httpformBase_0.List_0);
			Boolean_1 = true;
			Boolean_2 = httpformBase_0.Boolean_2;
			Boolean_3 = httpformBase_0.Boolean_3;
		}

		public virtual void PrepareRequest(HTTPRequest httprequest_0)
		{
			throw new NotImplementedException();
		}

		public virtual byte[] GetData()
		{
			throw new NotImplementedException();
		}
	}
}
